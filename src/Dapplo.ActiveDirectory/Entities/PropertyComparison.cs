// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;
using Dapplo.ActiveDirectory.Extensions;

namespace Dapplo.ActiveDirectory.Entities;

/// <summary>
///     Represents a Property equals "(operatingSystem=Windows XP)"
///     There is also a Negate method, which makes "(!operatingSystem=Windows XP)"
/// </summary>
public class PropertyComparison : QueryElement
{
	internal PropertyComparison(Property property, Value value = null, Comparisons comparison = Comparisons.EqualTo, Query parent = null) : base(parent)
	{
		Comparison = comparison;
		Property = property;
		Value = value;
	}

	/// <summary>
	/// The used comparison
	/// </summary>
	public Comparisons Comparison { get; set; }

	/// <summary>
	/// Returns true if the comparison is a "not"
	/// </summary>
	public bool Not => Comparison.ToString().StartsWith("Not");

	/// <summary>
	/// The property to compare
	/// </summary>
	public Property Property { get; set; }

	/// <summary>
	/// The value to compare
	/// </summary>
	public Value Value { get; set; }

	/// <summary>
	/// Invert the comparison
	/// </summary>
	public void Negate()
	{
		switch (Comparison)
		{
			case Comparisons.NotEqualTo:
				Comparison = Comparisons.EqualTo;
				break;
			case Comparisons.NotGreaterOrEquals:
				Comparison = Comparisons.GreaterThanOrEqualTo;
				break;
			case Comparisons.NotLessOrEquals:
				Comparison = Comparisons.LessThanOrEqualTo;
				break;
			case Comparisons.EqualTo:
				Comparison = Comparisons.NotEqualTo;
				break;
			case Comparisons.GreaterThanOrEqualTo:
				Comparison = Comparisons.NotGreaterOrEquals;
				break;
			case Comparisons.LessThanOrEqualTo:
				Comparison = Comparisons.NotLessOrEquals;
				break;
			default:
				throw new ArgumentException();
		}
	}

	/// <summary>
	///     Create a string representation of this property comparison
	/// </summary>
	/// <returns></returns>
	public override string ToString()
	{
		var builder = new StringBuilder();
		if (Not)
		{
			builder.Append("(!");
		}
		builder.Append($"({Property}{Comparison.EnumValueOf()}{Value.RawValue})");
		if (Not)
		{
			builder.Append(")");
		}
		return builder.ToString();
	}
}