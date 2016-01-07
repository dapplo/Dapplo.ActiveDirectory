using System;
using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory.Internal
{
	/// <summary>
	/// Extensions for working with some enums
	/// </summary>
	internal static class EnumExtension
	{
		public static string EnumMemberOf<Enum>(this Enum enumerationValue)
		{
			EnumMemberAttribute[] attributes = (EnumMemberAttribute[])enumerationValue.GetType().GetField(enumerationValue.ToString()).GetCustomAttributes(typeof(EnumMemberAttribute), false);
			return attributes.Length > 0 ? attributes[0].Value : enumerationValue.ToString();
		}
	}
}
