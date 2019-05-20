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

#region using

using System;
using System.Text;
using Dapplo.ActiveDirectory.Extensions;

#endregion

namespace Dapplo.ActiveDirectory.Entities
{
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
}