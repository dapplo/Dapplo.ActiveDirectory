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

using System;
using Dapplo.LogFacade;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// A simple non mutable container for values
	/// This will do all the encoding and other validations
	/// </summary>
	public class Value
	{
		private static Value _any = new Value("*");
		private string _value;

		/// <summary>
		/// The raw value is used in the query, and is the encoded value
		/// </summary>
		public string RawValue
		{
			get
			{
				return _value;
			}
		}

		/// <summary>
		/// Private constructor so this 
		/// </summary>
		/// <param name="value"></param>
		private Value(string value)
		{
			_value = value;
		}

		/// <summary>
		/// Encode special characters
		/// </summary>
		/// <param name="value">Value before adding *</param>
		/// <returns>string</returns>
		private static string EncodeSpecialChars(string value)
		{
			return value.Replace("*", @"\2A").Replace("(", @"\28").Replace(")", @"\29").Replace(@"\", @"\5C").Replace("\0", @"\00");
		}

		/// <summary>
		/// Decode special characters
		/// </summary>
		/// <param name="value">Encoded value</param>
		/// <returns>string</returns>
		private static string DecodeSpecialChars(string value)
		{
			return value.Replace(@"\2A", "*").Replace(@"\28", "(").Replace(@"\29", ")").Replace(@"\5C", @"\").Replace(@"\00", "\0");
		}

		/// <summary>
		/// The resulting value will make it possible to search for properties which start with the specified value
		/// </summary>
		/// <param name="value">your value</param>
		/// <returns>Value</returns>
		public static Value StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			return new Value($"{EncodeSpecialChars(value)}*");
		}

		/// <summary>
		/// The resulting value will make it possible to search for properties which end with the specified value
		/// </summary>
		/// <param name="value">your value</param>
		/// <returns>Value</returns>
		public static Value EndsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			return new Value($"*{EncodeSpecialChars(value)}");
		}

		/// <summary>
		/// The resulting value will make it possible to search for properties which contain specified value
		/// </summary>
		/// <param name="value">your value</param>
		/// <returns>Value</returns>
		public static Value Contain(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			return new Value($"*{EncodeSpecialChars(value)}*");
		}

		/// <summary>
		/// The resulting value uses the passed string without conversion
		/// </summary>
		/// <param name="value">your value</param>
		/// <returns>Value</returns>
		public static Value Raw(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			return new Value(value);
		}

		/// <summary>
		/// The resulting value will make it possible to search for properties which have a value (are not empty)
		/// </summary>
		/// <param name="value">your value</param>
		/// <returns>Value</returns>
		public static Value Any
		{
			get
			{
				return _any;
			}
		}

		/// <summary>
		/// Add implicit casting from string
		/// </summary>
		/// <param name="value">string</param>
		static public implicit operator Value(string value)
		{
			return new Value(EncodeSpecialChars(value));
		}
		/// <summary>
		/// Add implicit casting from string
		/// </summary>
		/// <param name="value">string</param>
		static public implicit operator Value(int value)
		{
			return new Value(value.ToString());
		}


		/// <summary>
		/// Add implicit casting from enum
		/// </summary>
		/// <param name="value">string</param>
		static public implicit operator Value(Enum value)
		{
			return new Value(value.EnumValueOf());
		}

		/// <summary>
		/// Add implicit casting to string
		/// </summary>
		/// <param name="value">string</param>
		static public implicit operator string(Value value)
		{
			return value.ToString();
		}

		/// <summary>
		/// The to string makes a readable string (decoding is done)
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return DecodeSpecialChars(_value);
		}
	}
}
