// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.ActiveDirectory.Enums;

namespace Dapplo.ActiveDirectory;

/// <summary>
/// Extend your interface / type with this to use in the execute / update
/// </summary>
public interface IAdObject
{
	/// <summary>
	/// This is THE unique property in the ADS which can be used
	/// to find and or update the returned entry.
	/// Use the AdProperty attribute to specify the properties you want mapped.
	/// </summary>
	[AdProperty(AdProperties.Id)]
	string Id { get; set; }
}