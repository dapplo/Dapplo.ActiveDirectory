// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;

namespace Dapplo.ActiveDirectory.Tests.Entities
{
	/// <summary>
	///     Simple predefined object for user information
	/// </summary>
	public interface IUser : IAdObject
	{
		[AdProperty(UserProperties.DisplayName)]
		string DisplayName { get; set; }

		[AdProperty(UserProperties.DistinguishedName)]
		string DistinguishedName { get; set; }

		[AdProperty(UserProperties.MemberOfGroups)]
		IList<DistinguishedName> Groups { get; set; }

		[AdProperty(UserProperties.Surname)]
		string Name { get; set; }

		[AdProperty(UserProperties.TelephoneNumber)]
		string TelephoneNumber { get; set; }

		[AdProperty(UserProperties.Thumbnail)]
		byte[] Thumbnail { get; set; }

		[AdProperty(UserProperties.AccountExpires)]
		DateTimeOffset? AccountExpires { get; set; }
	}
}