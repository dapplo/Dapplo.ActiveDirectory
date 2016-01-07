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
