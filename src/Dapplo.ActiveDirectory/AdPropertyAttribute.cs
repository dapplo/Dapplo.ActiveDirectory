//  Dapplo - building blocks for desktop applications
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

#region using

using System;

#endregion

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	///     Attribute to specify which AD property is stored in which property.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class AdPropertyAttribute : Attribute
	{
		/// <summary>
		///     Specify an Enum value, which correspons to an AD propertname, to make maping to this class property possible.
		/// </summary>
		/// <param name="adPropertyName"></param>
		public AdPropertyAttribute(object adPropertyName)
		{
			if (adPropertyName == null)
			{
				throw new ArgumentNullException(nameof(adPropertyName));
			}
			if (!adPropertyName.GetType().IsEnum)
			{
				throw new ArgumentException("Should be an enum value", nameof(adPropertyName));
			}
			AdProperty = (Enum) adPropertyName;
		}

		/// <summary>
		///     The AD property name for this property
		/// </summary>
		public Enum AdProperty { get; }
	}
}