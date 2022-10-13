// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Dapplo.ActiveDirectory.Internal;

/// <summary>
/// Com interface for the IAdsLargeInteger
/// </summary>
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FDispatchable)]
[Guid("B8C787CA-9BDD-11D0-852C-00C04FD8D503")]
[ComImport]
public interface IADsSecurityDescriptor
{
    /// <summary>
    /// the revision number assigned to the security descriptor.
    /// </summary>
    [DispId(2)]
    int Revision
    {
        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        get;
        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        set;
    }

    /// <summary>
    /// the Security_Descriptor_Control flag
    /// </summary>
    [DispId(3)]
    int Control
    {
        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        get;
        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        set;
    }

    /// <summary>
    /// the owner of the object associated with the security descriptor
    /// </summary>
    [DispId(4)]
    string Owner
    {
        [DispId(4)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
        [DispId(4)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [param: MarshalAs(UnmanagedType.BStr)]
        set;
    }

    /// <summary>
    /// the flag that indicates if the owner data is derived by a default mechanism
    /// </summary>
    [DispId(5)]
    bool OwnerDefaulted
    {
        [DispId(5)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        get;
        [DispId(5)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        set;
    }

    /// <summary>
    /// the group that owns the object associated with the security descriptor
    /// </summary>
    [DispId(6)]
    string Group
    {
        [DispId(6)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
        [DispId(6)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [param: MarshalAs(UnmanagedType.BStr)]
        set;
    }

    /// <summary>
    /// the flag that indicates if the group data is derived by a default mechanism
    /// </summary>
    [DispId(7)]
    bool GroupDefaulted
    {
        [DispId(7)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        get;
        [DispId(7)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        set;
    }

    /// <summary>
    /// the discretionary ACL associated with the security descriptor
    /// </summary>
    [DispId(8)]
    object DiscretionaryAcl
    {
        [DispId(8)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [return: MarshalAs(UnmanagedType.IDispatch)]
        get;
        [DispId(8)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [param: MarshalAs(UnmanagedType.IDispatch)]
        set;
    }

    /// <summary>
    /// the flag that indicates if the DACL is derived from a default mechanism
    /// </summary>
    [DispId(9)]
    bool DaclDefaulted
    {
        [DispId(9)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        get;
        [DispId(9)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        set;
    }

    /// <summary>
    /// the system ACL associated with the security descriptor
    /// </summary>
    [DispId(10)]
    object SystemAcl
    {
        [DispId(10)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [return: MarshalAs(UnmanagedType.IDispatch)]
        get;
        [DispId(10)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [param: MarshalAs(UnmanagedType.IDispatch)]
        set;
    }

    /// <summary>
    /// the flag that indicates if the SACL is derived from a default mechanism
    /// </summary>
    [DispId(11)]
    bool SaclDefaulted
    {
        [DispId(11)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        get;
        [DispId(11)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        set;
    }

    /// <summary>
    /// Copies the security descriptor
    /// </summary>
    /// <returns>object</returns>
    [DispId(12)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object CopySecurityDescriptor();
}