using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Entities
{
	public enum Operators
	{
		[EnumMember(Value = "")]
		None,
		[EnumMember(Value = "&")]
		And,
		[EnumMember(Value = "|")]
		Or,
	}
}
