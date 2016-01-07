/*
	Dapplo - building blocks for desktop applications
	Copyright (C) 2015-2016 Dapplo

	For more information see: http://dapplo.net/
	Dapplo repositories are hosted on GitHub: https://github.com/dapplo

	This file is part of Dapplo.ActiveDirectory

	Dapplo.ActiveDirectory is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Dapplo.Exchange is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.ActiveDirectory.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Internal
{
	/// <summary>
	/// Extensions for working with some enums
	/// </summary>
	internal static class EnumExtension
	{
		public static string EnumMemberOf<T>(this T enumerationValue) where T : struct, IComparable, IConvertible, IFormattable
		{
			if (!typeof (T).IsEnum)
			{
				throw new ArgumentException("Parameter must be an enum", nameof(enumerationValue));
			}
			var attributes = (EnumMemberAttribute[])enumerationValue.GetType().GetField(enumerationValue.ToString(CultureInfo.InvariantCulture)).GetCustomAttributes(typeof(EnumMemberAttribute), false);
			return attributes.Length > 0 ? attributes[0].Value : enumerationValue.ToString(CultureInfo.InvariantCulture);
		}
	}
}
