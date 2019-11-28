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

using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Enums
{
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
}