using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory
{
	public enum UserProperties
	{
		[EnumMember(Value = "givenName")]
		GivenName,
		[EnumMember(Value = "sn")]
		Surname,
		[EnumMember(Value = "department")]
		Department,
		[EnumMember(Value = "l")]
		Location,
		[EnumMember(Value = "telephoneNumber")]
		TelephoneNumber,
		[EnumMember(Value = "sAMAccountname")]
		Username,
		[EnumMember(Value = "mail")]
		EmailAddress
	}
}
