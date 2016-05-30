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


#endregion

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
		public static int SizeLimit { get; set; } = 0;

		/// <summary>
		/// Specify if the search is cached
		/// </summary>
		public static bool CacheResults { get; set; } = false;
	}
}