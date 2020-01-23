// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.ActiveDirectory.Extensions;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	///     Attribute to specify which AD property is stored in which property.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class AdPropertyAttribute : Attribute
	{
		/// <summary>
		///     Specify an Enum value, which corresponds to an AD property name, to make mapping to this class property possible.
		/// </summary>
		/// <param name="adPropertyName"></param>
		public AdPropertyAttribute(object adPropertyName)
		{
			if (adPropertyName == null)
			{
				throw new ArgumentNullException(nameof(adPropertyName));
			}
			if (adPropertyName.GetType().IsEnum)
			{
				AdProperty = ((Enum) adPropertyName).EnumValueOf().ToLowerInvariant();
				return;
			}
			AdProperty = adPropertyName.ToString().ToLowerInvariant();
		}

		/// <summary>
		///     The AD property name for this property
		/// </summary>
		public string AdProperty { get; }
	}
}