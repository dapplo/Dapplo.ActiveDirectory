// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.ActiveDirectory.Internal;
using System;
using System.DirectoryServices;
using System.Linq;
using Dapplo.ActiveDirectory.Entities;

namespace Dapplo.ActiveDirectory.Extensions;

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