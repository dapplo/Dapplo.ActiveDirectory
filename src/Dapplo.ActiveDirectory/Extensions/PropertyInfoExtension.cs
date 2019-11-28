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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dapplo.ActiveDirectory.Extensions
{
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
}