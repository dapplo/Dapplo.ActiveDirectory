using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Entities
{
	public enum Comparisons
	{
		[EnumMember(Value = "=")]
		Equals,
		[EnumMember(Value = ">=")]
		GreaterOrEquals,
		[EnumMember(Value = "<=")]
		LessOrEquals,
		[EnumMember(Value = "=")]
		NotEquals,
		[EnumMember(Value = ">=")]
		NotGreaterOrEquals,
		[EnumMember(Value = "<=")]
		NotLessOrEquals,
	}
}
