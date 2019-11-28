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

using Dapplo.ActiveDirectory.Internal;
using System;
using System.DirectoryServices;
using System.Linq;
using Dapplo.ActiveDirectory.Entities;

namespace Dapplo.ActiveDirectory.Extensions
{
    /// <summary>
    /// Extensions to handle the properties of a DirectoryEntry
    /// </summary>
    public static class DirectoryEntryPropertyExtensions
    {
        /// <summary>
        /// Convert the specified property to the target type
        /// </summary>
        /// <param name="directoryEntry">DirectoryEntry</param>
        /// <param name="propertyName">string</param>
        /// <param name="targetType">Type</param>
        /// <returns>object</returns>
        public static object ConvertProperty(this DirectoryEntry directoryEntry, string propertyName, Type targetType = null)
        {
            targetType ??= typeof(string);
            bool isId = propertyName == "uSNChanged" || propertyName == "uSNCreated";
            var value = directoryEntry.Properties[propertyName].Value;
            var valueType = value.GetType();

            if (targetType.IsAssignableFrom(valueType))
            {
                return value;
            }

            if (targetType == typeof(string))
            {
                return value switch
                {
                    IAdsLargeInteger largeIntegerToString when isId => largeIntegerToString.ToLong()?.ToString() ?? "n.a.",
                    IAdsLargeInteger largeIntegerToString => largeIntegerToString.ToDateTimeOffset()?.ToString() ?? "empty",
                    IADsSecurityDescriptor securityDescriptor => $"{securityDescriptor.Group}-{securityDescriptor.Owner}",
                    object[] objects => string.Join(",", objects.Select(o => o.ToString())),
                    byte[] bytes => string.Join(",", bytes),
                    _ => value.ToString(),
                };
            }

            if (valueType == typeof(DateTime) && targetType == typeof(DateTimeOffset))
            {
                return (DateTimeOffset) (DateTime) value;
            }

            if (value is IAdsLargeInteger largeInteger)
            {
                if (isId)
                {
                    return largeInteger.ToLong();
                }
                if (targetType == typeof(DateTime?))
                {
                    return largeInteger.ToDateTime();
                }
                if (targetType == typeof(DateTime))
                {
                    var dateTime = largeInteger.ToDateTime();
                    return dateTime ?? DateTime.MinValue;
                }
                if (targetType == typeof(DateTimeOffset?))
                {
                    return largeInteger.ToDateTimeOffset();
                }
                if (targetType == typeof(DateTimeOffset))
                {
                    var dateTimeOffset = largeInteger.ToDateTimeOffset();
                    return dateTimeOffset ?? DateTimeOffset.MinValue;
                }
            }
            
            if (valueType.IsArray || targetType.IsGenericType)
            {
                var values = (object[])value;

                if (targetType.GenericTypeArguments[0] == typeof(DistinguishedName))
                {
                    return values.Select(x => (DistinguishedName)(x as string)).ToList();
                }

                return values.Select(x => Convert.ChangeType(x, targetType)).ToList();
            }

            return Convert.ChangeType(value, targetType);
        }
    }
}
