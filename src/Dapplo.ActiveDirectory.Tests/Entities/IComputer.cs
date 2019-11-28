//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2019 Dapplo
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