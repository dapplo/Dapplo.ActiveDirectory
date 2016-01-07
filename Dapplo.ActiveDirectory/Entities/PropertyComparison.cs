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

using Dapplo.ActiveDirectory.Internal;
using System;

namespace Dapplo.ActiveDirectory.Entities
{
	/// <summary>
	/// Represents a Property equals "(operatingSystem=Windows XP)"
	/// There is also a Negate method, which makes "(!operatingSystem=Windows XP)"
	/// </summary>
	public class PropertyComparison
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

		internal PropertyComparison(string property, string value = null, Comparisons comparison = Comparisons.Equals)
		{
			Comparison = comparison;
			Property = property;
			Value = value;
		}

		public override string ToString()
		{
			var negative = Not? "!" : "";
			var value = string.IsNullOrEmpty(Value) ? "*" : Value;
			return $"({negative}{Property}{Comparison.EnumMemberOf()}{value})";
		}

		public void Negate()
		{
			switch (Comparison)
			{
				case Comparisons.NotEquals:
					Comparison = Comparisons.Equals;
					break;
				case Comparisons.NotGreaterOrEquals:
					Comparison = Comparisons.GreaterOrEquals;
					break;
				case Comparisons.NotLessOrEquals:
					Comparison = Comparisons.LessOrEquals;
					break;
				case Comparisons.Equals:
					Comparison = Comparisons.NotEquals;
					break;
				case Comparisons.GreaterOrEquals:
					Comparison = Comparisons.NotGreaterOrEquals;
					break;
				case Comparisons.LessOrEquals:
					Comparison = Comparisons.NotLessOrEquals;
					break;
				default:
					throw new ArgumentException();
			}
		}

	}
}
