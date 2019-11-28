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

namespace Dapplo.ActiveDirectory.Extensions
{
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
}
