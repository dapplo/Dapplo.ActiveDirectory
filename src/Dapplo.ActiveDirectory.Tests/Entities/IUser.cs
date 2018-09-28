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

using System.Collections.Generic;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;

#endregion

namespace Dapplo.ActiveDirectory.Tests.Entities
{
	/// <summary>
	///     Simple predefined object for user information
	/// </summary>
	public interface IUser : IAdObject
	{
		[AdProperty(UserProperties.DisplayName)]
		string Displayname { get; set; }

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
	}
}