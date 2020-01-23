// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.ActiveDirectory.Internal;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	///     Some configuration settings
	/// </summary>
	public static class ActiveDirectoryGlobals
	{
		/// <summary>
		/// Specify the global page size for searches
		/// Note: 0 means no page size, and retrieve everything up to the SizeLimit
		/// Everything smaller than the SizeLimit (but gt 0) will make the search ignore the size limit!
		/// </summary>
		public static int PageSize { get; set; } = 50;

		/// <summary>
		/// Specify the global size limit for searches
		/// Note:
		/// 0 means no limit
		/// If size limit is large than page size, this limit is ignored!!!
		/// </summary>
		public static int SizeLimit { get; set; }

		/// <summary>
		/// Specify if the search is cached
		/// </summary>
		public static bool CacheResults { get; set; } = false;

		/// <summary>
		/// The prefix used for the Ldap URI building
		/// </summary>
		public static string LdapUriPrefix = "LDAP://";

		/// <summary>
		/// This is the factory used to generate all the objects in the result
		/// </summary>
		public static IAdObjectFactory Factory { get; set; } = new SimpleFactory();

	}
}