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

using System;
using Dapplo.LogFacade;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// A simple non mutable container for properties
	/// This will do all the encoding and other validations
	/// </summary>
	public class Property
	{
		private string _property;

		/// <summary>
		/// Private constructor so this 
		/// </summary>
		/// <param name="value"></param>
		private Property(string value)
		{
			_property = value;
		}

		/// <summary>
		/// This creates a property with the LDAP_MATCHING_RULE_BIT_AND rule
		/// </summary>
		/// <param name="property">The property</param>
		/// <returns>Property</returns>
		public static Property BitAnd(Property property)
		{
			return new Property($"{property}:1.2.840.113556.1.4.803:");
		}

		/// <summary>
		/// This creates a property with the LDAP_MATCHING_RULE_BIT_OR rule
		/// </summary>
		/// <param name="property">The property</param>
		/// <returns>Property</returns>
		public static Property BitOr(Property property)
		{
			return new Property($"{property}:1.2.840.113556.1.4.804:");
		}

		/// <summary>
		/// This creates a property with the LDAP_MATCHING_RULE_TRANSITIVE_EVAL rule
		/// </summary>
		/// <param name="property">The property</param>
		/// <returns>Property</returns>
		public static Property TransitiveEval(Property property)
		{
			return new Property($"{property}:1.2.840.113556.1.4.1941:");
		}

		/// <summary>
		/// This creates a property with the LDAP_MATCHING_RULE_IN_CHAIN rule
		/// </summary>
		/// <param name="property">The property</param>
		/// <returns>Property</returns>
		public static Property InChain(Property property)
		{
			return TransitiveEval(property);
		}
		/// <summary>
		/// This creates a property with a rule, looks like this: "property:rule:"
		/// </summary>
		/// <param name="property">The property</param>
		/// <param name="rule">The property</param>
		/// <returns>Property</returns>
		public static Property WithRule(Property property, string rule)
		{
			return new Property($"{property}:rule:");
		}


		/// <summary>
		/// This creates a property with the LDAP_MATCHING_RULE_DN_WITH_DATA rule
		/// </summary>
		/// <param name="property">The property</param>
		/// <returns>Property</returns>
		public static Property DnWithData(string property)
		{
			return new Property($"{property}:1.2.840.113556.1.4.2253:");
		}

		/// <summary>
		/// Add implicit casting from string
		/// </summary>
		/// <param name="property">string</param>
		static public implicit operator Property(string property)
		{
			return new Property(property);
		}

		/// <summary>
		/// Add implicit casting from enum
		/// </summary>
		/// <param name="property">Enum</param>
		static public implicit operator Property(Enum property)
		{
			return new Property(property.EnumValueOf());
		}

		/// <summary>
		/// Add implicit casting to string
		/// </summary>
		/// <param name="property">Property</param>
		static public implicit operator string(Property property)
		{
			return property.ToString();
		}

		/// <summary>
		/// The to string makes a readable string (decoding is done)
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return _property;
		}
	}
}
