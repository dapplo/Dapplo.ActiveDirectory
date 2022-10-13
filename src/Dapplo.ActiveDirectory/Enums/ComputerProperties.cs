// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable 1591

using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Enums;

/// <summary>
///		These are some possible user "attributes".
///		If you need your own you can just create an enum like this and use it.
/// </summary>
public enum ComputerProperties
{
	[EnumMember(Value = "dNSHostName")] HostName,
	Name,
	Location,
	OperatingSystem,
	OperatingSystemServicePack,
	OperatingSystemVersion,
	Description,
	WhenCreated,
	WhenChanged
}