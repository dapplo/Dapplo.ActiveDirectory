// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable 1591

using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Enums
{
	/// <summary>
	///     These are some possible user "attributes", but if you need your own you can just create an enum like this and use
	///     it.
	///     Here is a list of user attributes which should always be available as they are standard:
	///     http://www.selfadsi.org/user-attributes.htm
	/// </summary>
	public enum UserProperties
	{
		[EnumMember(Value = "accountExpires")] AccountExpires,
		[EnumMember(Value = "department")] Department,
		[EnumMember(Value = "description")] Description,
		[EnumMember(Value = "displayName")] DisplayName,
		[EnumMember(Value = "distinguishedName")] DistinguishedName,
		[EnumMember(Value = "mail")] EmailAddress,
		[EnumMember(Value = "givenName")] GivenName,
		[EnumMember(Value = "initials")] Initials,
		[EnumMember(Value = "ipPhone")] IpTelephoneNumber,
		[EnumMember(Value = "lastLogonTimestamp")] LastLogonTimestamp,
		[EnumMember(Value = "l")] Location,
		[EnumMember(Value = "memberOf")] MemberOfGroups,
		[EnumMember(Value = "pager")] Pager,
		[EnumMember(Value = "pwdLastSet")] PwdLastSet,
		[EnumMember(Value = "postalCode")] PostalCode,
		[EnumMember(Value = "sn")] Surname,
		[EnumMember(Value = "streetAddress")] StreetAddress,
		[EnumMember(Value = "telephoneNumber")] TelephoneNumber,
		[EnumMember(Value = "thumbnailphoto")] Thumbnail,
		[EnumMember(Value = "userAccountControl")] UserAccountControl,
		[EnumMember(Value = "sAMAccountname")] Username
	}
}