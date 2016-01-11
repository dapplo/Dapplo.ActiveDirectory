/*
	Dapplo - building blocks for desktop applications
	Copyright (C) 2015-2016 Dapplo

	For more information see: http://dapplo.net/
	Dapplo repositories are hosted on GitHub: https://github.com/dapplo

	This file is part of Dapplo.ActiveDirectory

	Dapplo.ActiveDirectory is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Dapplo.Exchange is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.ActiveDirectory.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa366101.aspx
	/// </summary>
	public enum DistinguishedNameAttributes
	{
		[EnumMember(Value = "DC")]
		DomainComponent,
		[EnumMember(Value = "CN")]
		CommonName,
		[EnumMember(Value = "OU")]
		OrganizationalUnitName,
		[EnumMember(Value = "O")]
		OrganizationName,
		[EnumMember(Value = "STREET")]
		StreetAddress,
		[EnumMember(Value = "L")]
		LocalityName,
		[EnumMember(Value = "ST")]
		StateOrProvinceName,
		[EnumMember(Value = "C")]
		CountryName,
		[EnumMember(Value = "UID")]
		Userid
	}
}
