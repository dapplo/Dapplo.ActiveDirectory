// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.ActiveDirectory.Internal;
using System;

namespace Dapplo.ActiveDirectory.Extensions;

/// <summary>
/// Extensions to help with AD date time objects
/// </summary>
public static class AdDateTimeExtensions
{
    /// <summary>
    /// Convert an IAdsLargeInteger to a long
    /// </summary>
    /// <param name="largeInteger">IAdsLargeInteger</param>
    /// <returns>long?</returns>
    public static long? ToLong(this IAdsLargeInteger largeInteger)
    {
        long dateLong = (largeInteger.HighPart << 32) + largeInteger.LowPart;
        if (dateLong <= 0 || dateLong == 9223372036854775807)
        {
            return null;
        }
        return dateLong;
    }

    /// <summary>
    /// Convert an IAdsLargeInteger to a DateTimeOffset
    /// </summary>
    /// <param name="largeInteger">IAdsLargeInteger</param>
    /// <returns>DateTimeOffset?</returns>
    public static DateTimeOffset? ToDateTimeOffset(this IAdsLargeInteger largeInteger)
    {
        var dateLong = largeInteger.ToLong();
        if (!dateLong.HasValue)
        {
            return null;
        }
        return DateTimeOffset.FromFileTime(dateLong.Value);
    }

    /// <summary>
    /// Convert an IAdsLargeInteger to a DateTime
    /// </summary>
    /// <param name="largeInteger">IAdsLargeInteger</param>
    /// <returns>DateTime?</returns>
    public static DateTime? ToDateTime(this IAdsLargeInteger largeInteger)
    {
        var dateLong = largeInteger.ToLong();
        if (!dateLong.HasValue)
        {
            return null;
        }
        return DateTime.FromFileTimeUtc(dateLong.Value);
    }
}