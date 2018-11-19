﻿//  Dapplo - building blocks for desktop applications
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

using System.Runtime.Serialization;

#endregion

namespace Dapplo.ActiveDirectory.Entities
{
	/// <summary>
	///     The comparisons for a property
	/// </summary>
	public enum Comparisons
	{
		[EnumMember(Value = "=")] EqualTo,
		[EnumMember(Value = ">=")] GreaterThanOrEqualTo,
		[EnumMember(Value = "<=")] LessThanOrEqualTo,
		[EnumMember(Value = "=")] NotEqualTo,
		[EnumMember(Value = ">=")] NotGreaterOrEquals,
		[EnumMember(Value = "<=")] NotLessOrEquals
	}
}