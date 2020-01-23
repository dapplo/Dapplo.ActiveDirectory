// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.ActiveDirectory
{
    /// <summary>
    /// This interface can be used to implement your own factory for the result objects of a query
    /// </summary>
    public interface IAdObjectFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAdContainer"></typeparam>
        /// <returns></returns>
        TAdContainer Generate<TAdContainer>();
    }
}
