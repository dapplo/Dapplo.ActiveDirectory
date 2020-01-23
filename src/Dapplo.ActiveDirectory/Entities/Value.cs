// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.ActiveDirectory.Extensions;

namespace Dapplo.ActiveDirectory.Entities
{
	/// <summary>
	///     A simple non mutable container for values
	///     This will do all the encoding and other validations
	/// </summary>
	public class Value
	{
		/// <summary>
		///     Private constructor so this
		/// </summary>
		/// <param name="value"></param>
		private Value(string value)
		{
			RawValue = value;
		}

		/// <summary>
		///     The resulting value will make it possible to search for properties which have a value (are not empty)
		/// </summary>
		/// <returns>Value</returns>
		public static Value Any { get; } = new Value("*");

		/// <summary>
		///     The raw value is used in the query, and is the encoded value
		/// </summary>
		public string RawValue { get; }

		/// <summary>
		///     The resulting value will make it possible to search for properties which contain specified value
		/// </summary>
		/// <param name="value">your value</param>
		/// <returns>Value</returns>
		public static Value Contains(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
			return new Value($"*{EncodeSpecialChars(value)}*");
		}

		/// <summary>
		///     Decode special characters
		/// </summary>
		/// <param name="value">Encoded value</param>
		/// <returns>string</returns>
		private static string DecodeSpecialChars(string value)
		{
			return value.Replace(@"\2A", "*").Replace(@"\28", "(").Replace(@"\29", ")").Replace(@"\5C", @"\").Replace(@"\00", "\0");
		}

		/// <summary>
		///     Encode special characters
		/// </summary>
		/// <param name="value">Value before adding *</param>
		/// <returns>string</returns>
		private static string EncodeSpecialChars(string value)
		{
			return value.Replace("*", @"\2A").Replace("(", @"\28").Replace(")", @"\29").Replace(@"\", @"\5C").Replace("\0", @"\00");
		}

		/// <summary>
		///     The resulting value will make it possible to search for properties which end with the specified value
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
		///     Add implicit casting from string
		/// </summary>
		/// <param name="value">string</param>
		public static implicit operator Value(string value)
		{
			return new Value(EncodeSpecialChars(value));
		}

		/// <summary>
		///     Add implicit casting from string
		/// </summary>
		/// <param name="value">string</param>
		public static implicit operator Value(int value)
		{
			return new Value(value.ToString());
		}


		/// <summary>
		///     Add implicit casting from enum
		/// </summary>
		/// <param name="value">string</param>
		public static implicit operator Value(Enum value)
		{
			return new Value(value.EnumValueOf());
		}

		/// <summary>
		///     Add implicit casting to string
		/// </summary>
		/// <param name="value">string</param>
		public static implicit operator string(Value value)
		{
			return value.ToString();
		}

		/// <summary>
		///     The resulting value uses the passed string without conversion
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
		///     The resulting value will make it possible to search for properties which start with the specified value
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
		///     The to string makes a readable string (decoding is done)
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return DecodeSpecialChars(RawValue);
		}
	}
}