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

#pragma warning disable 1591

#region using

using System;

#endregion

namespace Dapplo.ActiveDirectory.Enums
{
	/// <summary>
	///     This can be used with queries using the userAccountControl
	///     See <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa772300.aspx">here</a> for more information.
	/// </summary>
	[Flags]
	public enum UserAccountControlFlags
	{
		Script = 0x0001,
		AccountDisabled = 0x0002,
		HomedirRequired = 0x0008,
		Lockout = 0x0010,
		PasswordNotRequired = 0x0020,
		PasswordCantChange = 0x0040,
		EncryptedTextPasswordAllowed = 0x0080,
		TempDuplicateAccount = 0x0100,
		NormalAccount = 0x0200,
		InterdomainTrustAccount = 0x0800,
		WorkstationTrustAccount = 0x1000,
		ServerTrustAccount = 0x2000,
		DontExpirePassword = 0x10000,
		MajorityNodeSetLogonAccount = 0x20000,
		SmartcardRequired = 0x40000,
		TrustedForDelegation = 0x80000,
		NotDelegated = 0x100000,
		UseDesKeyOnly = 0x200000,
		DontRequirePreauth = 0x400000,
		PasswordExpired = 0x800000,
		TrustedToAuthForDelegation = 0x1000000
	}
}