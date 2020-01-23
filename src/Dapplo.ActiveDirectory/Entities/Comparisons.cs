﻿// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable 1591

using System.Runtime.Serialization;

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