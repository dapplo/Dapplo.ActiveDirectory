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
using System;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// This specifies a query
	/// </summary>
	public class Query : QueryElement
	{
		private readonly IList<QueryElement> _elements = new List<QueryElement>();

		/// <summary>
		/// The operator for this query
		/// </summary>
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
			return CreateAnd().IsUser().EqualTo(UserProperties.Username, username);
		}

		/// <summary>
		/// Create a filter for the hostname
		/// </summary>
		/// <param name="hostname"></param>
		/// <returns>Query</returns>
		public static Query ComputerFilter(string hostname)
		{
			return CreateAnd().IsComputer().EqualTo(ComputerProperties.HostName, hostname);
		}

		/// <summary>
		/// Create a query
		/// </summary>
		/// <returns>Query</returns>
		private static Query Create(Operators queryOperator, Query parent = null)
		{
			return new Query(queryOperator, parent);
		}

		/// <summary>
		/// Create an AND query
		/// </summary>
		/// <returns>Query</returns>
		public static Query CreateAnd()
		{
			return Create(Operators.And);
		}

		/// <summary>
		/// Create an OR query
		/// </summary>
		/// <returns>Query</returns>
		public static Query CreateOr()
		{
			return Create(Operators.Or);
		}

		/// <summary>
		/// Internal constructor, used by Or/And and Create
		/// </summary>
		/// <param name="queryOperator"></param>
		/// <param name="parent"></param>
		internal Query(Operators queryOperator = Operators.And, Query parent = null) : base(parent)
		{
			Operator = queryOperator;
		}

		/// <summary>
		/// Add an AND sub query to the current query
		/// </summary>
		/// <returns>Query which is a sub-query of the parent</returns>
		public Query And()
		{
			var andQuery = Create(Operators.And, this);
			_elements.Add(andQuery);
			return andQuery;
		}

		/// <summary>
		/// Add an OR sub query to the current query
		/// </summary>
		/// <returns>Query which is a sub-query of the parent</returns>
		public Query Or()
		{
			var orQuery = Create(Operators.Or, this);
			_elements.Add(orQuery);
			return orQuery;
		}

		/// <summary>
		/// Add a compare to the current query for a property and value
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <param name="comparison">Comparisons to specify how to compare</param>
		/// <returns>Query</returns>
		public Query Compare(string property, string value, Comparisons comparison)
		{
			var propertyEqual = new PropertyComparison(property, value, comparison, this);
			_elements.Add(propertyEqual);
			return this;
		}

		/// <summary>
		/// Add a compare to the current query for a property and value
		/// </summary>
		/// <param name="property">Enum</param>
		/// <param name="value"></param>
		/// <param name="comparison">Comparisons to specify how to compare</param>
		/// <returns>Query</returns>
		public Query Compare(Enum property, string value, Comparisons comparison)
		{
			return Compare(property.EnumValueOf(), value, comparison);
		}

		/// <summary>
		/// Property should not be equal to the value
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <returns>Query</returns>
		public Query NotEqualTo(string property, string value)
		{
			return Compare(property, value, Comparisons.NotEqualTo);
		}

		/// <summary>
		/// Property should not be equal to the value
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <returns>Query</returns>
		public Query NotEqualTo(Enum property, string value)
		{
			return Compare(property, value, Comparisons.NotEqualTo);
		}

		/// <summary>
		/// Property should be equal to the value
		/// </summary>
		/// <param name="property">Enum instance</param>
		/// <param name="value"></param>
		/// <returns>Query</returns>
		public Query EqualTo(string property, string value)
		{
			return Compare(property, value, Comparisons.EqualTo);
		}

		/// <summary>
		/// Property should be equal to the value
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <returns>Query</returns>
		public Query EqualTo(Enum property, string value)
		{
			return Compare(property, value, Comparisons.EqualTo);
		}

		/// <summary>
		/// Check for an absent property
		/// </summary>
		/// <param name="property"></param>
		/// <returns>Query</returns>
		public Query Empty(string property)
		{
			return Compare(property, "*", Comparisons.NotEqualTo);
		}

		/// <summary>
		/// Check for an absent property
		/// </summary>
		/// <param name="property">Enum</param>
		/// <returns>Query</returns>
		public Query Empty(Enum property)
		{
			return Compare(property, "*", Comparisons.NotEqualTo);
		}

		/// <summary>
		/// Check for the existance of a property
		/// </summary>
		/// <param name="property"></param>
		/// <returns>Query</returns>
		public Query Any(string property)
		{
			return Compare(property, "*", Comparisons.EqualTo);
		}

		/// <summary>
		/// Check for the existance of a property
		/// </summary>
		/// <param name="property">Enum</param>
		/// <returns>Query</returns>
		public Query Any(Enum property)
		{
			return Compare(property, "*", Comparisons.EqualTo);
		}

		/// <summary>
		/// Check if the property is equal to or less than the value
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <returns>Query</returns>
		public Query LessThanOrEqualTo(string property, string value)
		{
			return Compare(property, value, Comparisons.LessThanOrEqualTo);
		}

		/// <summary>
		/// Check if the property is equal to or less than the value
		/// </summary>
		/// <param name="property">Enum</param>
		/// <param name="value"></param>
		/// <returns>Query</returns>
		public Query LessThanOrEqualTo(Enum property, string value)
		{
			return Compare(property, value, Comparisons.LessThanOrEqualTo);
		}

		/// <summary>
		/// Check if the property is equal to or greater than the value
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <returns>Query</returns>
		public Query GreaterOrEquals(string property, string value)
		{
			return Compare(property, value, Comparisons.GreaterThanOrEqualTo);
		}

		/// <summary>
		/// Check if the property is equal to or greater than the value
		/// </summary>
		/// <param name="property">Enum</param>
		/// <param name="value"></param>
		/// <returns>Query</returns>
		public Query GreaterOrEquals(Enum property, string value)
		{
			return Compare(property, value, Comparisons.GreaterThanOrEqualTo);
		}

		/// <summary>
		/// Add an comparison for the objectCatefory, default is user
		/// </summary>
		/// <param name="category">value from the ObjectCategories enum</param>
		/// <returns>Query</returns>
		public Query IsObjectCategory(ObjectCategories category)
		{
			return Compare("objectCategory", category.EnumValueOf(), Comparisons.EqualTo);
		}

		/// <summary>
		/// Make the query look for the user objectCategory
		/// </summary>
		/// <returns>Query</returns>
		public Query IsUser()
		{
			return Compare("objectCategory", ObjectCategories.User.EnumValueOf(), Comparisons.EqualTo);
		}

		/// <summary>
		/// Make the query look for the computer objectCategory
		/// </summary>
		/// <returns>Query</returns>
		public Query IsComputer()
		{
			return Compare("objectCategory", ObjectCategories.Computer.EnumValueOf(), Comparisons.EqualTo);
		}

		/// <summary>
		/// Build this query element as a string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			// Opening bracket & operator
			if (_elements.Count > 0)
			{
				stringBuilder.Append($"({Operator.EnumValueOf()}");
			}

			foreach (var element in _elements)
			{
				stringBuilder.Append(element);
			}

			// Closing bracket
			if (_elements.Count > 0)
			{
				stringBuilder.Append(")");
			}
			
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Build the query to a string.
		/// This will go up the parent chain until there is none specified and starts calling ToString() on all elements going down the chain again.
		/// </summary>
		/// <returns>string with the complete query</returns>
		public string Build()
		{
			if (Parent != null)
			{
				return Parent.Build();
			}
			return ToString();
		}
	}
}
