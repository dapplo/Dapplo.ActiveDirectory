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
	along with Dapplo.ActiveDirectory.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System.Text;
using Dapplo.ActiveDirectory.Entities;
using System;
using Dapplo.LogFacade;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// Query is a query builder for Active Directory queries
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
		/// Create a query for the username
		/// </summary>
		/// <param name="username">Windows username (without the domain)</param>
		/// <returns>Query</returns>
		public static Query UsernameFilter(Value username)
		{
			return CreateAnd().IsUser().EqualTo(UserProperties.Username, username);
		}

		/// <summary>
		/// Create a filter for the computer name
		/// </summary>
		/// <param name="hostname">Value with the hostname (without domain!)</param>
		/// <returns>Query</returns>
		public static Query ComputerFilter(Value hostname)
		{
			return CreateAnd().IsComputer().EqualTo(ComputerProperties.Name, hostname);
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
		/// <param name="queryOperator">Operators</param>
		/// <param name="parent">The parent query, if any</param>
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
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">Value to compare</param>
		/// <param name="comparison">Comparisons to specify how to compare</param>
		/// <returns>Query</returns>
		public Query Compare(Property property, Value value, Comparisons comparison)
		{
			var propertyEqual = new PropertyComparison(property, value, comparison, this);
			_elements.Add(propertyEqual);
			return this;
		}

		/// <summary>
		/// Property should not be equal to the value
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">value to compare</param>
		/// <returns>Query</returns>
		public Query NotEqualTo(Property property, Value value)
		{
			return Compare(property, value, Comparisons.NotEqualTo);
		}

		/// <summary>
		/// Property should be equal to the value
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">value to compare</param>
		/// <returns>Query</returns>
		public Query EqualTo(Property property, Value value)
		{
			return Compare(property, value, Comparisons.EqualTo);
		}

		/// <summary>
		/// Check for an absent property
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <returns>Query</returns>
		public Query Empty(Property property)
		{
			return Compare(property, Value.Any, Comparisons.NotEqualTo);
		}

		/// <summary>
		/// Check for the existance of a property
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <returns>Query</returns>
		public Query Any(Property property)
		{
			return Compare(property, Value.Any, Comparisons.EqualTo);
		}

		/// <summary>
		/// Check if the property is equal to or less than the value
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">value to compare to</param>
		/// <returns>Query</returns>
		public Query LessThanOrEqualTo(Property property, Value value)
		{
			return Compare(property, value, Comparisons.LessThanOrEqualTo);
		}

		/// <summary>
		/// Check if the property is equal to or greater than the value
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">value to compare to</param>
		/// <returns>Query</returns>
		public Query GreaterOrEquals(Property property, Value value)
		{
			return Compare(property, value, Comparisons.GreaterThanOrEqualTo);
		}


		/// <summary>
		/// Add an comparison for the objectClass, if possible use IsObjectCategory as this has a higher chance of being indexed.
		/// </summary>
		/// <param name="objectClass">string with the objectClass</param>
		/// <returns>Query</returns>
		public Query IsObjectClass(Value objectClass)
		{
			return Compare("objectClass", objectClass, Comparisons.EqualTo);
		}

		/// <summary>
		/// Add an comparison for the objectCategory
		/// </summary>
		/// <param name="objectCategory">Value for the objectCategory</param>
		/// <returns>Query</returns>
		public Query IsObjectCategory(Value objectCategory)
		{
			return Compare("objectCategory", objectCategory, Comparisons.EqualTo);
		}

		/// <summary>
		/// Make the query look for the user objectCategory
		/// </summary>
		/// <returns>Query</returns>
		public Query IsUser()
		{
			return IsObjectClass(ClassNames.User);
		}
		/// <summary>
		/// Make the query look for active users only
		/// </summary>
		/// <returns>Query</returns>
		public Query IsActiveUser()
		{
			return IsObjectClass(ClassNames.User).NotEqualTo(Property.BitAnd(UserProperties.UserAccountControl), (int)UserAccountControlFlags.AccountDisabled);
		}


		/// <summary>
		/// Make the query look for the computer objectCategory
		/// </summary>
		/// <returns>Query</returns>
		public Query IsComputer()
		{
			return IsObjectCategory(ClassNames.Computer);
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
