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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Dapplo.ActiveDirectory.Internal
{
    /// <summary>
    /// Com interface for the IAdsLargeInteger
    /// </summary>
    // , TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FDispatchable)
    [Guid("B8C787CA-9BDD-11D0-852C-00C04FD8D503")]
    [ComImport]
    public interface IADsSecurityDescriptor
    {
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

        [DispId(12)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        [return: MarshalAs(UnmanagedType.IDispatch)]
        object CopySecurityDescriptor();
    }
}