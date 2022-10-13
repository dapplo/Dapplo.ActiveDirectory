﻿// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.ActiveDirectory.Extensions;

namespace Dapplo.ActiveDirectory.Entities;

/// <summary>
///     A simple non mutable container for properties
///     This will do all the encoding and other validations
/// </summary>
public class Property
{
	private readonly string _property;

	/// <summary>
	///     Private constructor so this
	/// </summary>
	/// <param name="value"></param>
	private Property(string value)
	{
		_property = value;
	}

	/// <summary>
	///     This creates a property with the LDAP_MATCHING_RULE_BIT_AND rule
	/// </summary>
	/// <param name="property">The property</param>
	/// <returns>Property</returns>
	public static Property BitAnd(Property property)
	{
		return new Property($"{property}:1.2.840.113556.1.4.803:");
	}

	/// <summary>
	///     This creates a property with the LDAP_MATCHING_RULE_BIT_OR rule
	/// </summary>
	/// <param name="property">The property</param>
	/// <returns>Property</returns>
	public static Property BitOr(Property property)
	{
		return new Property($"{property}:1.2.840.113556.1.4.804:");
	}


	/// <summary>
	///     This creates a property with the LDAP_MATCHING_RULE_DN_WITH_DATA rule
	/// </summary>
	/// <param name="property">The property</param>
	/// <returns>Property</returns>
	public static Property DnWithData(Property property)
	{
		return new Property($"{property}:1.2.840.113556.1.4.2253:");
	}

	/// <summary>
	///     This creates a property with the LDAP_MATCHING_RULE_IN_CHAIN rule
	/// </summary>
	/// <param name="property">The property</param>
	/// <returns>Property</returns>
	public static Property InChain(Property property)
	{
		return TransitiveEval(property);
	}

	/// <summary>
	///     Add implicit casting from string
	/// </summary>
	/// <param name="property">string</param>
	public static implicit operator Property(string property)
	{
		return new Property(property);
	}

	/// <summary>
	///     Add implicit casting from enum
	/// </summary>
	/// <param name="property">Enum</param>
	public static implicit operator Property(Enum property)
	{
		return new Property(property.EnumValueOf());
	}

	/// <summary>
	///     Add implicit casting to string
	/// </summary>
	/// <param name="property">Property</param>
	public static implicit operator string(Property property)
	{
		return property.ToString();
	}

	/// <summary>
	///     The to string makes a readable string (decoding is done)
	/// </summary>
	/// <returns></returns>
	public override string ToString()
	{
		return _property;
	}

	/// <summary>
	///     This creates a property with the LDAP_MATCHING_RULE_TRANSITIVE_EVAL rule
	/// </summary>
	/// <param name="property">The property</param>
	/// <returns>Property</returns>
	public static Property TransitiveEval(Property property)
	{
		return new Property($"{property}:1.2.840.113556.1.4.1941:");
	}

	/// <summary>
	///     This creates a property with a rule, looks like this: "property:rule:"
	/// </summary>
	/// <param name="property">The property</param>
	/// <param name="rule">The property</param>
	/// <returns>Property</returns>
	public static Property WithRule(Property property, Property rule)
	{
		return new Property($"{property}:{rule}:");
	}
}