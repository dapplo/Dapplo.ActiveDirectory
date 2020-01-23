// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable 1591

using System;

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