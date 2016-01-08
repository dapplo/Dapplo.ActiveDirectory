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

	Dapplo.Exchange is distributed in the hope that it will be useful,
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
using Dapplo.ActiveDirectory.Internal;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// A simple ActiveDirectory helper class
	/// </summary>
	public static class ActiveDirectoryExtensions
	{
		/// <summary>
		/// Use the ActiveDirectory with the supplied domain to query
		/// </summary>
		/// <param name="query">Query</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <param name="propertiesToLoad">An enumerable with the properties (as enum elements) to load</param>
		/// <returns>IEnumerable with a dictionary for the properties</returns>
		public static IEnumerable<IDictionary<string, object>> FindAll(this Query query, string domain = null, IEnumerable<Enum> propertiesToLoad = null)
		{
			return FindAll(query.Build(), domain, propertiesToLoad?.Select(x => x.EnumValueOf()));
		}

		/// <summary>
		/// Use the ActiveDirectory with the supplied domain to query
		/// </summary>
		/// <param name="query">Query</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <param name="propertiesToLoad">An enumerable with the properties to load, defaults include some user properties</param>
		/// <returns>IEnumerable with a dictionary for the properties</returns>
		public static IEnumerable<IDictionary<string, object>> FindAll(this Query query, string domain = null, IEnumerable<string> propertiesToLoad = null)
		{
			return FindAll(query.Build(), domain, propertiesToLoad);
		}

		public static readonly string[] UserProps = { "givenName", "sn", "department", "l", "telephoneNumber", "sAMAccountname", "mail" };

		/// <summary>
		/// Query the LDAP server for the supplied domain
		/// </summary>
		/// <param name="query">Query as string</param>
		/// <param name="domain">Domain for the LDAP server, if null the Environment.UserDomainName is used</param>
		/// <param name="propertiesToLoad">An enumerable with the properties to load, defaults include some user properties</param>
		/// <returns>IEnumerable with a dictionary for the properties</returns>
		public static IEnumerable<IDictionary<string, object>> FindAll(string query, string domain = null, IEnumerable<string> propertiesToLoad = null)
		{
			if (propertiesToLoad == null)
			{
				propertiesToLoad = UserProps;
			}
			using (var rootDirectory = new DirectoryEntry($"LDAP://{domain ?? Environment.UserDomainName}"))
			using (var searcher = new DirectorySearcher(rootDirectory, query, propertiesToLoad.ToArray()))
			using (var results = searcher.FindAll())
			{
				foreach (SearchResult result in results)
				{
					if (result?.Properties?.PropertyNames == null)
					{
						continue;
					}
					var properties =
						(from propertyName
						 in result.Properties.PropertyNames.Cast<string>()
						 select new
						 {
							 name = propertyName,
							 value = result.Properties[propertyName][0]
						 }).ToDictionary(x => x.name, x => x.value);
					yield return properties;
				}
			}
		}
	}
}
