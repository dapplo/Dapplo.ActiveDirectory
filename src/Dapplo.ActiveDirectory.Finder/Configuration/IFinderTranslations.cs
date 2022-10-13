// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

using System.ComponentModel;
using Dapplo.CaliburnMicro.Translations;
using Dapplo.Config.Language;

namespace Dapplo.ActiveDirectory.Finder.Configuration;

/// <summary>
///     The translations for Finder
/// </summary>
[Language("Finder")]
public interface IFinderTranslations : ILanguage, IConfigTranslations, ICoreTranslations
{
    /// <summary>
    ///     This describes the name of the configuration window and system tray icon
    /// </summary>
    [DefaultValue("Configuration")]
    string Configuration { get; }

    /// <summary>
    ///     The title of the application
    /// </summary>
    [DefaultValue("Finder")]
    string Title { get; }
}