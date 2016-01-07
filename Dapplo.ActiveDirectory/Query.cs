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
	along with Dapplo.ActiveDirectory.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System.Text;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Internal;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// This specifies a query
	/// </summary>
	public class Query
	{
		private readonly Query _parent;
		private readonly IList<PropertyComparison> _comparisons = new List<PropertyComparison>();

		public Operators Operator
		{
			get;
		}

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
		private static Query Create(Operators queryOperator)
		{
			return new Query(queryOperator);
		}

		/// <summary>
		/// Add an AND query
		/// </summary>
		/// <returns>Query</returns>
		public static Query And()
		{
			return Create(Operators.And);
		}

		/// <summary>
		/// Add an OR query
		/// </summary>
		/// <returns>Query</returns>
		public static Query Or()
		{
			return Create(Operators.Or);
		}

		internal Query(Operators queryOperator = Operators.And, Query parent = null)
		{
			_parent = parent;
			Operator = queryOperator;
		}

		public Query Compare(string property, string value = null, Comparisons comparison = Comparisons.Equals)
		{
			var propertyEqual = new PropertyComparison(property, value, comparison);
			_comparisons.Add(propertyEqual);
			return this;
		}

		public Query Compare(UserProperties userProperty, string value = null, Comparisons comparison = Comparisons.Equals)
		{
			return Compare(userProperty.EnumMemberOf(), value, comparison);
		}

		public Query Compare(ComputerProperties computerProperty, string value = null, Comparisons comparison = Comparisons.Equals)
		{
			return Compare(computerProperty.EnumMemberOf(), value, comparison);
		}

		/// <summary>
		/// Add an comparison for the objectCatefory, default is user
		/// </summary>
		/// <param name="category">value from the ObjectCategories enum</param>
		/// <param name="comparison">specify the comparison to make</param>
		/// <returns>Query</returns>
		public Query ObjectCategory(ObjectCategories category, Comparisons comparison = Comparisons.Equals)
		{
			var propertyEqual = new PropertyComparison("objectCategory", category.EnumMemberOf(), comparison);
			_comparisons.Add(propertyEqual);
			return this;
		}

		/// <summary>
		/// Make the query look for the user objectCategory
		/// </summary>
		/// <param name="comparison">specify the comparison to make</param>
		/// <returns>Query</returns>
		public Query UserCategory(Comparisons comparison = Comparisons.Equals)
		{
			var propertyEqual = new PropertyComparison("objectCategory", ObjectCategories.User.EnumMemberOf(), comparison);
			_comparisons.Add(propertyEqual);
			return this;
		}

		/// <summary>
		/// Make the query look for the computer objectCategory
		/// </summary>
		/// <param name="comparison">specify the comparison to make</param>
		/// <returns>Query</returns>
		public Query ComputerCategory(Comparisons comparison = Comparisons.Equals)
		{
			var propertyEqual = new PropertyComparison("objectCategory", ObjectCategories.Computer.EnumMemberOf(), comparison);
			_comparisons.Add(propertyEqual);
			return this;
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder($"({Operator.EnumMemberOf()}");
			foreach (var comparison in _comparisons)
			{
				stringBuilder.Append(comparison);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		public string Build()
		{
			if (_parent != null)
			{
				return _parent.Build();
			}
			return ToString();
		}
	}
}
