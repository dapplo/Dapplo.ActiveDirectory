//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2019 Dapplo
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

using System.Collections.Generic;
using System.Text;
using Dapplo.ActiveDirectory.Enums;
using Dapplo.ActiveDirectory.Extensions;

namespace Dapplo.ActiveDirectory.Entities
{
	/// <summary>
	///     Query is a query builder for Active Directory queries
	/// </summary>
	public class Query : QueryElement
	{
		private readonly IList<QueryElement> _elements = new List<QueryElement>();

		/// <summary>
		///     Internal constructor, used by Or/And and Create
		/// </summary>
		/// <param name="queryOperator">Operators</param>
		/// <param name="parent">The parent query, if any</param>
		internal Query(Operators queryOperator = Operators.And, Query parent = null) : base(parent)
		{
			Operator = queryOperator;
		}

		/// <summary>
		///     Add an AND sub query to the current query
		/// </summary>
		/// <returns>Query which is a sub-query of the parent</returns>
		public Query And
		{
			get
			{
				var andQuery = Create(Operators.And, this);
				_elements.Add(andQuery);
				return andQuery;
			}
		}

		/// <summary>
		///     AND query
		/// </summary>
		/// <returns>Query</returns>
		// ReSharper disable once InconsistentNaming
		public static Query AND => Create(Operators.And);

		/// <summary>
		///     The operator for this query
		/// </summary>
		public Operators Operator { get; }

		/// <summary>
		///     Add an OR sub query to the current query
		/// </summary>
		/// <returns>Query which is a sub-query of the parent</returns>
		public Query Or
		{
			get
			{
				var orQuery = Create(Operators.Or, this);
				_elements.Add(orQuery);
				return orQuery;
			}
		}

        /// <summary>
        ///     OR query
        /// </summary>
        /// <returns>Query</returns>
        // ReSharper disable once InconsistentNaming
		public static Query OR => Create(Operators.Or);

		/// <summary>
		///     Build the query to a string.
		///     This will go up the parent chain until there is none specified and starts calling ToString() on all elements going
		///     down the chain again.
		/// </summary>
		/// <returns>string with the complete query</returns>
		public string Build()
		{
			return Parent != null ? Parent.Build() : ToString();
		}

		/// <summary>
		///     Create a query
		/// </summary>
		/// <returns>Query</returns>
		private static Query Create(Operators queryOperator, Query parent = null)
		{
			return new Query(queryOperator, parent);
		}

		/// <summary>
		///     Create a query for a computer, optionally you can specify the name
		/// </summary>
		/// <param name="hostname">hostname (without domain!)</param>
		/// <returns>Query</returns>
		public static Query ForComputer(Value hostname = null)
		{
			var query = AND.WhereIsComputer();
			if (hostname != null)
			{
				query.WhereEqualTo(ComputerProperties.Name, hostname);
			}

			return query;
		}

		/// <summary>
		///     Create a query for an user, optionally you can specify the name
		/// </summary>
		/// <param name="username">Windows username (without the domain)</param>
		/// <returns>Query</returns>
		public static Query ForUser(Value username = null)
		{
			var query = AND.WhereIsUser();
			if (username != null)
			{
				query.WhereEqualTo(UserProperties.Username, username);
			}
			return query;
		}

		/// <summary>
		///     Build this query element as a string
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
		///     Equal to
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">Value to compare</param>
		/// <param name="comparison">Comparisons to specify how to compare</param>
		/// <returns>Query</returns>
		public Query Where(Property property, Value value, Comparisons comparison)
		{
			var propertyEqual = new PropertyComparison(property, value, comparison, this);
			_elements.Add(propertyEqual);
			return this;
		}

		/// <summary>
		///     Make the query look for enabled accounts
		/// </summary>
		/// <returns>Query</returns>
		public Query WhereAccountDisabled()
		{
			return WhereEqualTo(Property.BitAnd(UserProperties.UserAccountControl), (int) UserAccountControlFlags.AccountDisabled);
		}

		/// <summary>
		///     Make the query look for disabled accounts
		/// </summary>
		/// <returns>Query</returns>
		public Query WhereAccountEnabled()
		{
			return WhereNot(Property.BitAnd(UserProperties.UserAccountControl), (int) UserAccountControlFlags.AccountDisabled);
		}

		/// <summary>
		///     Check for the existance of a property
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <returns>Query</returns>
		public Query WhereAny(Property property)
		{
			return Where(property, Value.Any, Comparisons.EqualTo);
		}

		/// <summary>
		///     Check for an absent property
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <returns>Query</returns>
		public Query WhereEmpty(Property property)
		{
			return Where(property, Value.Any, Comparisons.NotEqualTo);
		}

		/// <summary>
		///     Property should be equal to the value
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">value to compare</param>
		/// <returns>Query</returns>
		public Query WhereEqualTo(Property property, Value value)
		{
			return Where(property, value, Comparisons.EqualTo);
		}

		/// <summary>
		///     Greater than or equal to
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">value to compare to</param>
		/// <returns>Query</returns>
		public Query WhereGte(Property property, Value value)
		{
			return Where(property, value, Comparisons.GreaterThanOrEqualTo);
		}


		/// <summary>
		///     Make the query look for the computer objectCategory
		/// </summary>
		/// <returns>Query</returns>
		public Query WhereIsComputer()
		{
			return WhereObjectCategoryIs(ClassNames.Computer);
		}

		/// <summary>
		///     Make the query look for the user objectCategory
		/// </summary>
		/// <returns>Query</returns>
		public Query WhereIsUser()
		{
			return WhereObjectClassIs(ClassNames.User);
		}

		/// <summary>
		///     Less than or equal to
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">value to compare to</param>
		/// <returns>Query</returns>
		public Query WhereLte(Property property, Value value)
		{
			return Where(property, value, Comparisons.LessThanOrEqualTo);
		}

		/// <summary>
		///     Not equal to
		/// </summary>
		/// <param name="property">property as Property, enum or string</param>
		/// <param name="value">value to compare</param>
		/// <returns>Query</returns>
		public Query WhereNot(Property property, Value value)
		{
			return Where(property, value, Comparisons.NotEqualTo);
		}

		/// <summary>
		///     Add an comparison for the objectCategory
		/// </summary>
		/// <param name="objectCategory">Value for the objectCategory</param>
		/// <returns>Query</returns>
		public Query WhereObjectCategoryIs(Value objectCategory)
		{
			return Where("objectCategory", objectCategory, Comparisons.EqualTo);
		}

		/// <summary>
		///     Add an comparison for the objectClass, if possible use IsObjectCategory as this has a higher chance of being
		///     indexed.
		/// </summary>
		/// <param name="objectClass">string with the objectClass</param>
		/// <returns>Query</returns>
		public Query WhereObjectClassIs(Value objectClass)
		{
			return Where("objectClass", objectClass, Comparisons.EqualTo);
		}
	}
}