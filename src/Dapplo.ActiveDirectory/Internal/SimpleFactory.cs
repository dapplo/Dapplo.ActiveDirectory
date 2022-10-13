// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dapplo.ActiveDirectory.Internal;

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