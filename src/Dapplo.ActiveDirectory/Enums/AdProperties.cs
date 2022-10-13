// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Enums;

/// <summary>
///     Ad generic properties, this is what every entry has
/// </summary>
public enum AdProperties
{
	/// <summary>
	/// Unique path in the active directory, which can be considered as the ID
	/// </summary>
	[EnumMember(Value = "adspath")] Id,

	/// <summary>
	/// The common name
	/// </summary>
	[EnumMember(Value = "cn")] CommonName,

	/// <summary>
	/// The OU for an element
	/// </summary>
	[EnumMember(Value = "ou")] OrganisationUnit,

	/// <summary>
	///     This is a constructed attribute, get this e.g. by calling
	///     <code>directoryEntry.RefreshCache(new[] { AdProperties.AllowedAttributes.EnumValueOf() });</code>
	/// </summary>
	[EnumMember(Value = "allowedAttributesEffective")] AllowedAttributesEffective
}