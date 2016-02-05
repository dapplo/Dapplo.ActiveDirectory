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

	Dapplo.ActiveDirectory is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.ActiveDirectory.  If not, see <http://www.gnu.org/licenses/>.
 */

using Dapplo.LogFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// This class helps with processing a DistinguishedName
	/// See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa366101.aspx
	/// </summary>
	public class DistinguishedName
	{
		private static IDictionary<string, DistinguishedNameAttributes> AttributeLookup = new Dictionary<string, DistinguishedNameAttributes>();

		public IList<KeyValuePair<DistinguishedNameAttributes, string>> RelativeDistinguishedNames
		{
			get;
		} = new List<KeyValuePair<DistinguishedNameAttributes, string>>();

		static DistinguishedName()
		{
			foreach(var attribute in Enum.GetValues(typeof(DistinguishedNameAttributes)).Cast<DistinguishedNameAttributes>()) {
				var key = attribute.EnumValueOf();
				AttributeLookup.Add(key, attribute);
			}
		}

		/// <summary>
		/// Create a DistinguishedName from a string representation
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
		/// Create a DistinguishedName, as the default constructor is private
		/// </summary>
		/// <returns>DistinguishedName</returns>
		public static DistinguishedName Create()
		{
			return new DistinguishedName();
		}

		/// <summary>
		/// Made private to force factory method
		/// </summary>
		private DistinguishedName()
		{

		}

		/// <summary>
		/// Add a CN
		/// </summary>
		/// <param name="value"></param>
		/// <returns>DistinguishedName</returns>
		public DistinguishedName CN(string value)
		{
			return Add(DistinguishedNameAttributes.CommonName, value);
		}

		/// <summary>
		/// Add a OU
		/// </summary>
		/// <param name="value"></param>
		/// <returns>DistinguishedName</returns>
		public DistinguishedName OU(string value)
		{
			return Add(DistinguishedNameAttributes.OrganizationalUnitName, value);
		}

		/// <summary>
		/// Add a DC
		/// </summary>
		/// <param name="value"></param>
		/// <returns>DistinguishedName</returns>
		public DistinguishedName DC(string value)
		{
			return Add(DistinguishedNameAttributes.DomainComponent, value);
		}

		/// <summary>
		/// Add whatever the DistinguishedNameAttributes has
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
			RelativeDistinguishedNames.Add(new KeyValuePair<DistinguishedNameAttributes, string>(distinguishedNameAttribute, value.Trim()));
			return this;
		}

		/// <summary>
		/// Add a "relative" distinguishedName, meaning it will parse a value like "DC=something" and add it
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
			RelativeDistinguishedNames.Add(new KeyValuePair<DistinguishedNameAttributes, string>(AttributeLookup[parts[0].Trim()], parts[1].Trim()));
			return this;
		}

		/// <summary>
		/// Create a string representation of the DistinguishedName
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var builder = new StringBuilder();
			foreach(var relativeDistinguishedName in RelativeDistinguishedNames)
			{
				builder.Append($"{relativeDistinguishedName.Key.EnumValueOf()}={relativeDistinguishedName.Value},");
			}
			// Remote trailing ,
			if (RelativeDistinguishedNames.Count > 0)
			{
				builder.Length -= 1;
			}
			return builder.ToString();
		}
	}
}
