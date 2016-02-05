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

using Dapplo.LogFacade;
using System;
using System.Text;

namespace Dapplo.ActiveDirectory.Entities
{
	/// <summary>
	/// Represents a Property equals "(operatingSystem=Windows XP)"
	/// There is also a Negate method, which makes "(!operatingSystem=Windows XP)"
	/// </summary>
	public class PropertyComparison : QueryElement
	{
		public Comparisons Comparison
		{
			get;
			set;
		}

		public bool Not
		{
			get
			{
				return Comparison.ToString().StartsWith("Not");
			}
		}

		public string Property
		{
			get;
			set;
		}

		public string Value
		{
			get;
			set;
		}

		internal PropertyComparison(string property, string value = null, Comparisons comparison = Comparisons.EqualTo, Query parent = null) : base(parent)
		{
			Comparison = comparison;
			Property = property;
			Value = value;
		}

		/// <summary>
		/// Create a string representation of this property comparison
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var builder = new StringBuilder();
			if (Not)
			{
				builder.Append("(!");
			}
			var value = string.IsNullOrEmpty(Value) ? "*" : Value;

			builder.Append($"({Property}{Comparison.EnumValueOf()}{value})");
			if (Not)
			{
				builder.Append(")");
			}
			return builder.ToString();
		}

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

	}
}
