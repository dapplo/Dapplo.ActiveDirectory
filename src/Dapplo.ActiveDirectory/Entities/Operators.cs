// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable 1591

using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Entities;

/// <summary>
///     the operators which are used inside a query
/// </summary>
public enum Operators
{
	[EnumMember(Value = "")] None,
	[EnumMember(Value = "&")] And,
	[EnumMember(Value = "|")] Or
}