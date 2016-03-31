//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
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

using System.Runtime.Serialization;

#endregion

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