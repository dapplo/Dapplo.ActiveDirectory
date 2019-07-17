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
using System.Collections.Generic;
using System.Reflection;

namespace Dapplo.ActiveDirectory.Internal
{
    /// <summary>
    /// This is used to generate implementations of property interfaces without logic
    /// Cannot handle other methods
    /// </summary>
    public class SimpleDictionaryProxy : DispatchProxy
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Implements the invoke
        /// </summary>
        /// <param name="targetMethod">MethodInfo</param>
        /// <param name="args">object array</param>
        /// <returns>object</returns>
        /// <exception cref="NotImplementedException">When a method is define on the interface, and this is called</exception>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            if (targetMethod.Name.StartsWith("get_"))
            {
                var key = targetMethod.Name.Substring(4);
                return _properties.ContainsKey(key) ? _properties[key] : null;
            }
            if (targetMethod.Name.StartsWith("set_"))
            {
                var key = targetMethod.Name.Substring(4);
                _properties[key] = args[0];
                return null;
            }
            throw new NotImplementedException(targetMethod.ToString());
        }

        /// <summary>
        /// Factory for a SimpleDictionaryProxy 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns>Proxy which implements TInterface via SimpleDictionaryProxy</returns>
        public static TInterface Create<TInterface>()
        {
            return DispatchProxy.Create<TInterface, SimpleDictionaryProxy>();
        }
    }
}
