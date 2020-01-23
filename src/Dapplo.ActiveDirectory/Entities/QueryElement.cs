// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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