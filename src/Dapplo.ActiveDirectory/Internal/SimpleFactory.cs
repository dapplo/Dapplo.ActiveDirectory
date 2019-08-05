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

using System;

namespace Dapplo.ActiveDirectory.Internal
{
    /// <inheritdoc />
    internal class SimpleFactory : IAdObjectFactory
    {
        /// <summary>
        /// Generate a SimpleDictionaryProxy when only an interface is passed
        /// </summary>
        /// <typeparam name="TAdContainer">The type to generate</typeparam>
        /// <returns>TAdContainer</returns>
        public TAdContainer Generate<TAdContainer>()
        {
            var instance = typeof(TAdContainer).IsInterface ?
                SimpleDictionaryProxy.Create<TAdContainer>() :
                Activator.CreateInstance<TAdContainer>();

            return instance;
        }
    }
}
