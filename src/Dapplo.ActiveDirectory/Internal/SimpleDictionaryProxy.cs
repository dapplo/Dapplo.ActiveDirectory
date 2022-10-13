// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dapplo.ActiveDirectory.Internal;

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