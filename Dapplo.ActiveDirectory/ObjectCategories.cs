using System.ComponentModel;
using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory
{
	public enum ObjectCategories
	{
		[EnumMember(Value = "user")]
		User,
		[EnumMember(Value = "computer")]
		Computer
	}
}
