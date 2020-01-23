// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.ActiveDirectory.Enums;

namespace Dapplo.ActiveDirectory.Tests.Entities
{
	/// <summary>
	///     Simple predefined object for Computer information
	/// </summary>
	public interface IComputer : IAdObject
	{
		[AdProperty(ComputerProperties.Description)]
		string Description { get; set; }

		[AdProperty(ComputerProperties.HostName)]
		string Hostname { get; set; }

		[AdProperty(ComputerProperties.Location)]
		string Location { get; set; }

		[AdProperty(ComputerProperties.OperatingSystem)]
		string OperatingSystem { get; set; }

		[AdProperty(ComputerProperties.OperatingSystemServicePack)]
		string OperatingSystemServicePack { get; set; }

		[AdProperty(ComputerProperties.WhenCreated)]
		DateTimeOffset WhenCreated { get; set; }
	}
}