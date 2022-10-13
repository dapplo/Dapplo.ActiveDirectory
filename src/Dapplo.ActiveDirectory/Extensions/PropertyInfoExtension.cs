// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dapplo.ActiveDirectory.Extensions;

/// <summary>
/// Extensions for PropertyInfo
/// </summary>
public static class PropertyInfoExtension
{
	/// <summary>
	/// Retrieve an attribute from a property
	/// </summary>
	/// <typeparam name="TAttribute">Type of the attribute</typeparam>
	/// <param name="memberInfo">MemberInfo</param>
	/// <param name="inherit">bool default true to also check inherit class attributes</param>
	/// <param name="includeInterfaces">bool default true if the interfaces of the declaring type also need to be checked</param>
	/// <returns>TAttribute or null</returns>
	public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo, bool inherit = true, bool includeInterfaces = true) where TAttribute : Attribute
	{
		var attribute = memberInfo.GetCustomAttribute<TAttribute>(inherit);
		if (attribute != null)
		{
			return attribute;
		}

		// If we didn't find the DefaultValueAttribute on the property, we check for the same property on the implementing interfaces
		if (!includeInterfaces || memberInfo.DeclaringType == null)
		{
			return null;
		}

		foreach (var interfaceType in memberInfo.DeclaringType.GetInterfaces())
		{
			var interfacePropertyInfo = interfaceType.GetProperty(memberInfo.Name);
			var attributeOnInterface = interfacePropertyInfo?.GetCustomAttribute<TAttribute>(false);
			if (attributeOnInterface != null)
			{
				return attributeOnInterface;
			}
		}

		return null;
	}

	/// <summary>
	/// Retrieve attributes from a property
	/// </summary>
	/// <typeparam name="TAttribute">Type of the attribute</typeparam>
	/// <param name="memberInfo">MemberInfo</param>
	/// <param name="inherit">bool default true to also check inherit class attributes</param>
	/// <param name="includeInterfaces">bool default true if the interfaces of the declaring type also need to be checked</param>
	/// <returns>IEnumerable of TAttribute</returns>
	public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MemberInfo memberInfo, bool inherit = true, bool includeInterfaces = true) where TAttribute : Attribute
	{
		var attributes = memberInfo.GetCustomAttributes<TAttribute>(inherit);
			
		// If we didn't find the DefaultValueAttribute on the property, we check for the same property on the implementing interfaces
		if (!includeInterfaces || memberInfo.DeclaringType == null)
		{
			return attributes;
		}

		foreach (var interfaceType in memberInfo.DeclaringType.GetInterfaces())
		{
			var interfacePropertyInfo = interfaceType.GetProperty(memberInfo.Name);
			var attributesOnInterface = interfacePropertyInfo?.GetCustomAttributes<TAttribute>(false);
			if (attributesOnInterface != null)
			{
				attributes = attributes.Concat(attributesOnInterface);
			}
		}

		return attributes;
	}
}