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

#region using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapplo.ActiveDirectory.Enums;
using Dapplo.ActiveDirectory.Extensions;

#endregion

namespace Dapplo.ActiveDirectory.Entities
{
	/// <summary>
	///     This class helps with processing a DistinguishedName
	///     See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa366101.aspx
	/// </summary>
	public class DistinguishedName : IEnumerable<KeyValuePair<DistinguishedNameAttributes, string>>
	{
		private static readonly IDictionary<string, DistinguishedNameAttributes> AttributeLookup = new Dictionary<string, DistinguishedNameAttributes>();

		static DistinguishedName()
		{
			foreach (var attribute in Enum.GetValues(typeof (DistinguishedNameAttributes)).Cast<DistinguishedNameAttributes>())
			{
				var key = attribute.EnumValueOf();
				AttributeLookup.Add(key, attribute);
			}
		}

		private readonly List<KeyValuePair<DistinguishedNameAttributes, string>> _relativeDistinguishedNames = new List<KeyValuePair<DistinguishedNameAttributes, string>>();

		/// <summary>
		///     Made private to force factory method
		/// </summary>
		private DistinguishedName()
		{
		}

		/// <summary>
		/// List of the distinguished names, this is just the collection of all available dn's
		/// </summary>
		public IEnumerable<KeyValuePair<DistinguishedNameAttributes, string>> RelativeDistinguishedNames => _relativeDistinguishedNames.AsReadOnly();

		/// <summary>
		/// Enumerator for the destinguished names
		/// </summary>
		/// <returns>IEnumerator</returns>
		public IEnumerator<KeyValuePair<DistinguishedNameAttributes, string>> GetEnumerator()
		{
			return _relativeDistinguishedNames.GetEnumerator();
		}

		/// <summary>
		/// IEnumerable implementation
		/// </summary>
		/// <returns>IEnumerator</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _relativeDistinguishedNames.GetEnumerator();
		}

		/// <summary>
		///     Add whatever the DistinguishedNameAttributes has
		/// </summary>
		/// <param name="distinguishedNameAttribute">DistinguishedNameAttributes</param>
		/// <param name="value"></param>
		/// <returns>DistinguishedName</returns>
		public DistinguishedName Add(DistinguishedNameAttributes distinguishedNameAttribute, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			_relativeDistinguishedNames.Add(new KeyValuePair<DistinguishedNameAttributes, string>(distinguishedNameAttribute, value.Trim()));
			return this;
		}

		/// <summary>
		///     Add a "relative" distinguishedName, meaning it will parse a value like "DC=something" and add it
		/// </summary>
		/// <param name="relativeDistinguishedName"></param>
		/// <returns>DistinguishedName</returns>
		public DistinguishedName Add(string relativeDistinguishedName)
		{
			if (relativeDistinguishedName == null)
			{
				throw new ArgumentNullException(nameof(relativeDistinguishedName));
			}
			var parts = relativeDistinguishedName.Split('=');
			_relativeDistinguishedNames.Add(new KeyValuePair<DistinguishedNameAttributes, string>(AttributeLookup[parts[0].Trim()],parts[1].Trim()));
			return this;
		}

		/// <summary>
		///     Add a CN
		/// </summary>
		/// <param name="value"></param>
		/// <returns>DistinguishedName</returns>
		public DistinguishedName Cn(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			return Add(DistinguishedNameAttributes.CommonName, value.Trim());
		}

		/// <summary>
		///     Create a DistinguishedName, as the default constructor is private
		/// </summary>
		/// <returns>DistinguishedName</returns>
		public static DistinguishedName Create()
		{
			return new DistinguishedName();
		}

		/// <summary>
		///     Create a DistinguishedName from a string representation
		/// </summary>
		/// <param name="distinguishedName"></param>
		/// <returns>DistinguishedName</returns>
		public static DistinguishedName CreateFrom(string distinguishedName)
		{
			var dn = new DistinguishedName();

			distinguishedName.Split(',').ToList().ForEach(x => dn.Add(x.Trim()));
			return dn;
		}

		/// <summary>
		///     Add a DC
		/// </summary>
		/// <param name="value"></param>
		/// <returns>DistinguishedName</returns>
		public DistinguishedName Dc(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			return Add(DistinguishedNameAttributes.DomainComponent, value. Trim());
		}

		/// <summary>
		///     Add implicit casting from string
		/// </summary>
		/// <param name="value">string</param>
		public static implicit operator DistinguishedName(string value)
		{
			return CreateFrom(value);
		}

		/// <summary>
		///     Add implicit casting to string
		/// </summary>
		/// <param name="value">string</param>
		public static implicit operator string(DistinguishedName value)
		{
			return value.ToString();
		}

		/// <summary>
		///     Add a OU
		/// </summary>
		/// <param name="value"></param>
		/// <returns>DistinguishedName</returns>
		public DistinguishedName Ou(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			return Add(DistinguishedNameAttributes.OrganizationalUnitName, value.Trim());
		}

		/// <summary>
		///     Create a string representation of the DistinguishedName
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var builder = new StringBuilder();
			foreach (var relativeDistinguishedName in RelativeDistinguishedNames)
			{
				builder.Append($"{relativeDistinguishedName.Key.EnumValueOf()}={relativeDistinguishedName.Value},");
			}
			// Remote trailing ,
			if (_relativeDistinguishedNames.Any())
			{
				builder.Length -= 1;
			}
			return builder.ToString();
		}
	}
}