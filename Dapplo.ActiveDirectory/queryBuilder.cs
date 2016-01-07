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
	along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
 */

using Dapplo.ActiveDirectory.Entities;
using System.Collections.Generic;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// Use this class to build and active directory query
	/// </summary>
	public class QueryBuilder
	{
		/// <summary>
		/// Default constructor is private, so the static factory methods need to be used.
		/// </summary>
		private QueryBuilder() { }

		/// <summary>
		/// Create a filter for the username
		/// </summary>
		/// <param name="username"></param>
		/// <returns>Query</returns>
		public static Query UsernameFilter(string username)
		{
			return And().UserCategory().Compare(UserProperties.Username, username);
		}

		/// <summary>
		/// Create a filter for the hostname
		/// </summary>
		/// <param name="hostname"></param>
		/// <returns>Query</returns>
		public static Query ComputerFilter(string hostname)
		{
			return And().ComputerCategory().Compare(ComputerProperties.HostName, hostname);
		}

		/// <summary>
		/// Add a query
		/// </summary>
		/// <returns>Query</returns>
		public static Query Query(Operators queryOperator)
		{
			return new Query(queryOperator);
		}

		/// <summary>
		/// Add an AND query
		/// </summary>
		/// <returns>Query</returns>
		public static Query And()
		{
			return Query(Operators.And);
		}

		/// <summary>
		/// Add an OR query
		/// </summary>
		/// <returns>Query</returns>
		public static Query Or()
		{
			return Query(Operators.Or);
		}
	}
}
