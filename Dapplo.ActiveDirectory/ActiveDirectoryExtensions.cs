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
using System.Runtime.Serialization;

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
		/// <typeparam name="T">Type to fill</typeparam>
		/// <param name="query">Query</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <param name="propertiesToLoad">An enumerable with the properties (as enum elements) to load</param>
		/// <returns>IList with the specified type</returns>
		public static IList<T> FindAll<T>(this Query query, string domain = null) where T : class
		{
			var queryString = query.Build();
			return FindInAd<T>(queryString, domain);
		}

		/// <summary>
		/// Query the LDAP server for the supplied domain
		/// </summary>
		/// <typeparam name="T">Type to fill, use DataMemberAttribute to mark the properties</typeparam>
		/// <param name="query">Query as string</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <param name="propertiesToLoad">An enumerable with the properties to load, defaults include some user properties</param>
		/// <returns>IList with the specified type</returns>
		private static IList<T> FindInAd<T>(string query, string domain = null) where T : class
		{
			var result = new List<T>();
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
					T instance = Activator.CreateInstance<T>();
					foreach(var propertyName in searchResult.Properties.PropertyNames.Cast<string>())
					{
						if (!typeMap.ContainsKey(propertyName))
						{
							Log.Verbose().WriteLine("No property {0} in {1}", propertyName, typeof(T).Name);
							continue;
						}
						var properties = searchResult.Properties[propertyName] as ResultPropertyValueCollection;

						if (properties.Count > 1)
						{
							typeMap[propertyName].SetValue(instance, properties.Cast<string>().Select(x => DistinguishedName.CreateFrom(x)).ToList());
						}
						else
						{
							typeMap[propertyName].SetValue(instance, properties[0]);
						}
					}
					result.Add(instance);
				}
			}
			return result;
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
				var dataMemberAttribute = propertyInfo.GetCustomAttribute<DataMemberAttribute>(true);
				if (dataMemberAttribute != null)
				{
					result.Add(dataMemberAttribute.Name.ToLowerInvariant(), propertyInfo);
				}
			}
			return result;
		}
	}
}
