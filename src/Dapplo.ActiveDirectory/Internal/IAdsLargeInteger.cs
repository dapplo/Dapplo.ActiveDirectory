// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using System.Security;

namespace Dapplo.ActiveDirectory.Internal;

/// <summary>
/// Com interface for the IAdsLargeInteger
/// </summary>
[ComImport, Guid("9068270b-0939-11d1-8be1-00c04fd8d503"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface IAdsLargeInteger
{
    /// <summary>
    /// Get the high part of the large integer
    /// </summary>
    long HighPart
    {
        [SuppressUnmanagedCodeSecurity]
        get; [SuppressUnmanagedCodeSecurity]
        set;
    }

    /// <summary>
    /// Get the log part of the large integer
    /// </summary>
    long LowPart
    {
        [SuppressUnmanagedCodeSecurity]
        get; [SuppressUnmanagedCodeSecurity]
        set;
    }
}