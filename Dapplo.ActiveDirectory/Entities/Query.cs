using System.Collections.Generic;
using Dapplo.ActiveDirectory.Internal;
using System.Text;

namespace Dapplo.ActiveDirectory.Entities
{
	/// <summary>
	/// This specifies a query
	/// </summary>
	public class Query
	{
		private Query _parent;
		private IList<PropertyComparison> _comparisons = new List<PropertyComparison>();

		public Operators Operator
		{
			get;
			set;
		} = Operators.And;

		internal Query(Operators queryOperator = Operators.And, Query parent = null)
		{
			_parent = parent;
			Operator = queryOperator;
		}

		public Query Compare(string property, string value = null, Comparisons comparison = Comparisons.Equals)
		{
			var propertyEqual = new PropertyComparison(property, value, comparison);
			_comparisons.Add(propertyEqual);
			return this;
		}

		public Query Compare(UserProperties userProperty, string value = null, Comparisons comparison = Comparisons.Equals)
		{
			return Compare(userProperty.EnumMemberOf(), value, comparison);
		}

		public Query Compare(ComputerProperties computerProperty, string value = null, Comparisons comparison = Comparisons.Equals)
		{
			return Compare(computerProperty.EnumMemberOf(), value, comparison);
		}

		/// <summary>
		/// Add an comparison for the objectCatefory, default is user
		/// </summary>
		/// <param name="category">value from the ObjectCategories enum</param>
		/// <param name="comparison">specify the comparison to make</param>
		/// <returns>Query</returns>
		public Query ObjectCategory(ObjectCategories category, Comparisons comparison = Comparisons.Equals)
		{
			var propertyEqual = new PropertyComparison("objectCategory", category.EnumMemberOf(), comparison);
			_comparisons.Add(propertyEqual);
			return this;
		}

		/// <summary>
		/// Make the query look for the user objectCategory
		/// </summary>
		/// <param name="comparison">specify the comparison to make</param>
		/// <returns>Query</returns>
		public Query UserCategory(Comparisons comparison = Comparisons.Equals)
		{
			var propertyEqual = new PropertyComparison("objectCategory", ObjectCategories.User.EnumMemberOf(), comparison);
			_comparisons.Add(propertyEqual);
			return this;
		}

		/// <summary>
		/// Make the query look for the computer objectCategory
		/// </summary>
		/// <param name="comparison">specify the comparison to make</param>
		/// <returns>Query</returns>
		public Query ComputerCategory(Comparisons comparison = Comparisons.Equals)
		{
			var propertyEqual = new PropertyComparison("objectCategory", ObjectCategories.Computer.EnumMemberOf(), comparison);
			_comparisons.Add(propertyEqual);
			return this;
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder($"({Operator.EnumMemberOf()}");
			foreach (var comparison in _comparisons)
			{
				stringBuilder.Append(comparison);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		public string Build()
		{
			if (_parent != null)
			{
				return _parent.Build();
			}
			return ToString();
		}
	}
}
