//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.ActiveDirectory
// 
//  Dapplo.ActiveDirectory is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.ActiveDirectory is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.ActiveDirectory. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;
using Dapplo.InterfaceImpl;
using Dapplo.LogFacade;
using Dapplo.Utils.Extensions;

#endregion

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	///     A simple ActiveDirectory helper class
	/// </summary>
	public static class ActiveDirectoryExtensions
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		///     Use the ActiveDirectory with the supplied domain to query
		/// </summary>
		/// <typeparam name="T">Type to fill, use AdPropertyAttribute to specify the mapping</typeparam>
		/// <param name="query">Query</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <returns>IEnumerable with the specified type</returns>
		public static IEnumerable<T> Execute<T>(this Query query, string domain = null) where T : IAdObject
		{
			return Execute<T>(query.Build(), domain);
		}

		/// <summary>
		///     Query the LDAP server for the supplied domain
		/// </summary>
		/// <typeparam name="TAdContainer">Type to fill, use AdPropertyAttribute to specify the mapping</typeparam>
		/// <param name="query">Query as string</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <returns>IList with the specified type</returns>
		private static IEnumerable<TAdContainer> Execute<TAdContainer>(string query, string domain = null) where TAdContainer : IAdObject
		{
			var typeMap = ProcessType(typeof (TAdContainer));
			Log.Info().WriteLine("Querying domain {0} with {1} into {2}", domain, query, typeof (TAdContainer).Name);
			var queryProperties = typeMap.Select(x => x.Key).ToArray();
			using (var rootDirectory = new DirectoryEntry($"LDAP://{domain ?? Environment.UserDomainName}"))
			using (var searcher = new DirectorySearcher(rootDirectory, query, queryProperties))
			using (var results = searcher.FindAll())
			{
				foreach (SearchResult searchResult in results)
				{
					Log.Verbose().WriteLine("Processing {0}", searchResult.Path);
					if (searchResult.Properties?.PropertyNames == null)
					{
						continue;
					}
					TAdContainer instance;
					if (!typeof (TAdContainer).IsInterface)
					{
						instance = Activator.CreateInstance<TAdContainer>();
					}
					else
					{
						instance = InterceptorFactory.New<TAdContainer>();
					}
					foreach (var propertyName in searchResult.Properties.PropertyNames.Cast<string>().Select(x => x.ToLowerInvariant()))
					{
						if (!typeMap.Contains(propertyName))
						{
							Log.Verbose().WriteLine("No property {0} in {1}", propertyName, typeof (TAdContainer).Name);
							continue;
						}
						var propertyInfos = typeMap[propertyName];
						var values = searchResult.Properties[propertyName].Cast<object>().ToArray();
						var value = values.Length > 1 ? values : values[0];
						if (value == null)
						{
							continue;
						}
						var valueType = value.GetType();
						foreach (var propertyInfo in propertyInfos)
						{
							if (propertyInfo.PropertyType.IsAssignableFrom(valueType))
							{
								propertyInfo.SetValue(instance, value);
							}
							else if (valueType == typeof (DateTime) && propertyInfo.PropertyType == typeof (DateTimeOffset))
							{
								var dateTime = (DateTime) value;
								propertyInfo.SetValue(instance, (DateTimeOffset) dateTime);
							}
							else if (valueType.IsArray || propertyInfo.PropertyType.IsGenericType)
							{
								if (propertyInfo.PropertyType.GenericTypeArguments[0] == typeof (DistinguishedName))
								{
									propertyInfo.SetValue(instance, values.Select(x => (DistinguishedName) (x as string)).ToList());
								}
								else
								{
									propertyInfo.SetValue(instance, values.Select(x => Convert.ChangeType(x, propertyInfo.PropertyType)).ToList());
								}
							}
							else
							{
								propertyInfo.SetValue(instance, Convert.ChangeType(value, propertyInfo.PropertyType));
							}
						}
					}
					yield return instance;
				}
			}
		}

		/// <summary>
		///     This is more for debugging, with retrieve the complete DirectoryEntry for the AdsPath
		/// </summary>
		/// <param name="adsPath">
		///     string with the AdsPath, this can be retrieved by setting AdProperty(AdProperties.Id) on a string
		///     property for the type passed to Execute
		/// </param>
		/// <returns>DirectoryEntry</returns>
		public static DirectoryEntry GetByAdsPath(string adsPath)
		{
			var directoryEntry = new DirectoryEntry(adsPath);

			if (Log.IsVerboseEnabled())
			{
				Log.Verbose().WriteLine("List of all properties and their value:");
				foreach (var propertyName in directoryEntry.Properties.PropertyNames.Cast<string>().OrderBy(x => x))
				{
					Log.Verbose().WriteLine($"{propertyName} = {{0}}", directoryEntry.Properties[propertyName].Value); //Cast<object>().ToArray());
				}
				directoryEntry.RefreshCache(new[] {AdProperties.AllowedAttributesEffective.EnumValueOf()});
				Log.Verbose().WriteLine("List of all writable properties:");
				foreach (var writableProperty in directoryEntry.Properties[AdProperties.AllowedAttributesEffective.EnumValueOf()].Cast<string>().OrderBy(x => x))
				{
					Log.Verbose().WriteLine("Writeable property: {0}", writableProperty);
				}
			}
			return directoryEntry;
		}

		/// <summary>
		///     Create a map for properties & the property name from the AD
		/// </summary>
		/// <param name="typeToFill"></param>
		/// <returns>ILookup with for each found property (key) an IEnumerable with PropertyInfo objects</returns>
		private static ILookup<string, PropertyInfo> ProcessType(Type typeToFill)
		{
			var properties = from implementingInterface in typeToFill.GetInterfaces().Concat(new[] {typeToFill})
				from property in implementingInterface.GetRuntimeProperties()
				select property;
			return properties.Distinct().Select(x => new KeyValuePair<string, PropertyInfo>(ReadAdProperty(x), x)).Where(x => x.Key != null).ToLookup(x => x.Key, x => x.Value);
		}

		/// <summary>
		///     Get the custom AdPropertyAttribute, and read the enum value in there
		/// </summary>
		/// <param name="memberInfo">MemberInfo</param>
		/// <returns>string with the ad property name</returns>
		private static string ReadAdProperty(MemberInfo memberInfo)
		{
			var adPropertyAttribute = memberInfo.GetCustomAttribute<AdPropertyAttribute>(true);
			return adPropertyAttribute?.AdProperty.EnumValueOf().ToLowerInvariant();
		}

		/// <summary>
		///     If you want to update information, the minimum what the passed container needs is a property with AdProperties.Id
		/// </summary>
		/// <typeparam name="TAdContainer">The type of the AD Container object</typeparam>
		/// <param name="adContainerObject">object which has the values to update</param>
		/// <param name="domain"></param>
		public static void Update<TAdContainer>(TAdContainer adContainerObject, string domain = null) where TAdContainer : IAdObject
		{
			var typeMap = ProcessType(typeof (TAdContainer));
			if (typeMap[AdProperties.Id.EnumValueOf()].Count() > 1)
			{
				throw new ArgumentException("Only one property can be marked with AdProperties.Id", nameof(adContainerObject));
			}
			var adspath = typeMap[AdProperties.Id.EnumValueOf()].First().GetValue(adContainerObject) as string;
			var directoryEntry = new DirectoryEntry(adspath);
			directoryEntry.RefreshCache(new[] {AdProperties.AllowedAttributesEffective.EnumValueOf()});

			var needsCommit = false;
			foreach (var propertyName in typeMap.Select(l => l.Key))
			{
				if (!directoryEntry.Properties["allowedAttributesEffective"].Contains(propertyName))
				{
					Log.Debug().WriteLine("Skipping non writable property {0}", propertyName);
					continue;
				}
				needsCommit = true;
				var propertyInfo = typeMap[propertyName].First();
				var property = directoryEntry.Properties[propertyName];

				Log.Debug().WriteLine("Updating {0}", propertyName);
				property.Value = propertyInfo.GetValue(adContainerObject);
			}
			if (needsCommit)
			{
				directoryEntry.CommitChanges();
			}
		}
	}
}