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

namespace Dapplo.ActiveDirectory.Entities
{
	/// <summary>
	///     This is the base class for all elements in a query
	/// </summary>
	public abstract class QueryElement
	{
		/// <summary>
		/// Constructor for an query element, with a reference to the parent
		/// </summary>
		/// <param name="parent">Query</param>
		protected QueryElement(Query parent)
		{
			Parent = parent;
		}

		/// <summary>
		///     The parent query for this element
		/// </summary>
		public Query Parent { get; private set; }
	}
}