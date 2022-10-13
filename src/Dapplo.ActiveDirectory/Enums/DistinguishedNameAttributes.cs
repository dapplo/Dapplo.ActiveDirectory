// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable 1591

using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Enums;

/// <summary>
///     See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa366101.aspx
/// </summary>
public enum DistinguishedNameAttributes
{
	[EnumMember(Value = "DC")] DomainComponent,
	[EnumMember(Value = "CN")] CommonName,
	[EnumMember(Value = "OU")] OrganizationalUnitName,
	[EnumMember(Value = "O")] OrganizationName,
	[EnumMember(Value = "STREET")] StreetAddress,
	[EnumMember(Value = "L")] LocalityName,
	[EnumMember(Value = "ST")] StateOrProvinceName,
	[EnumMember(Value = "C")] CountryName,
	[EnumMember(Value = "UID")] Userid
}