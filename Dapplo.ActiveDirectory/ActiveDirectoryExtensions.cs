/*
	Dapplo - building blocks for desktop applications
	Copyright (C) 2015-2016 Dapplo

	For more information see: http://dapplo.net/
	Dapplo repositories are hosted on GitHub: https://github.com/dapplo

	This file is part of Dapplo.ActiveDirectory

	Dapplo.ActiveDirectory is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Dapplo.ActiveDirectory is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.ActiveDirectory. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using Dapplo.LogFacade;
using System.Reflection;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// A simple ActiveDirectory helper class
	/// </summary>
	public static class ActiveDirectoryExtensions
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		/// Use the ActiveDirectory with the supplied domain to query
		/// </summary>
		/// <typeparam name="T">Type to fill, use AdPropertyAttribute to specify the mapping</typeparam>
		/// <param name="query">Query</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <returns>IEnumerable with the specified type</returns>
		public static IEnumerable<T> Execute<T>(this Query query, string domain = null) where T : class
		{
			return Execute<T>(query.Build(), domain);
		}

		/// <summary>
		/// Query the LDAP server for the supplied domain
		/// </summary>
		/// <typeparam name="T">Type to fill, use AdPropertyAttribute to specify the mapping</typeparam>
		/// <param name="query">Query as string</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <returns>IList with the specified type</returns>
		private static IEnumerable<T> Execute<T>(string query, string domain = null) where T : class
		{
			var typeMap = ProcessType(typeof(T));
			Log.Info().WriteLine("Querying domain {0} with {1} into {2}", domain, query, typeof(T).Name);
			using (var rootDirectory = new DirectoryEntry($"LDAP://{domain ?? Environment.UserDomainName}"))
			using (var searcher = new DirectorySearcher(rootDirectory, query, typeMap.Keys.ToArray()))
			using (var results = searcher.FindAll())
			{
				foreach (SearchResult searchResult in results)
				{
					if (searchResult?.Properties?.PropertyNames == null)
					{
						continue;
					}
					var instance = Activator.CreateInstance<T>();
					foreach(var propertyName in searchResult.Properties.PropertyNames.Cast<string>().Select(x => x.ToLowerInvariant()))
					{
						if (!typeMap.ContainsKey(propertyName))
						{
							Log.Verbose().WriteLine("No property {0} in {1}", propertyName, typeof(T).Name);
							continue;
						}
						var properties = searchResult.Properties[propertyName];
						var propertyInfo = typeMap[propertyName];
						if (properties.Count > 1)
						{
							var values = properties.Cast<string>();
							if (!propertyInfo.PropertyType.IsGenericType)
							{
								if (propertyInfo.PropertyType.GenericTypeArguments[0] == typeof (DistinguishedName))
								{
									typeMap[propertyName].SetValue(instance, values.Select(DistinguishedName.CreateFrom).ToList());
								}
								else
								{
									typeMap[propertyName].SetValue(instance, values.Select(x => Convert.ChangeType(x, propertyInfo.PropertyType)).ToList());
								}
							}
						}
						else
						{
							var value = Convert.ChangeType(properties[0], propertyInfo.PropertyType);
							typeMap[propertyName].SetValue(instance, value);
						}
					}
					yield return instance;
				}
			}
		}

		/// <summary>
		/// Create a map for properties & the property name from the AD
		/// </summary>
		/// <param name="typeToFill"></param>
		/// <returns></returns>
		private static IDictionary<string, PropertyInfo> ProcessType(Type typeToFill)
		{
			var result = new Dictionary<string, PropertyInfo>();

			foreach(var propertyInfo in typeToFill.GetProperties())
			{
				var adPropertyAttribute = propertyInfo.GetCustomAttribute<AdPropertyAttribute>(true);
				if (adPropertyAttribute != null)
				{
					result.Add(adPropertyAttribute.AdProperty.EnumValueOf().ToLowerInvariant(), propertyInfo);
				}
			}
			return result;
		}
	}
}
