// Dapplo - building blocks for desktop applications
// Copyright (C) 2017-2019  Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Finder
// 
// Finder is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Finder is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Finder. If not, see <http://www.gnu.org/licenses/lgpl.txt>.
// 

#region Usings

using System.ComponentModel;
using Dapplo.CaliburnMicro.Translations;
using Dapplo.Config.Language;

#endregion

namespace Dapplo.ActiveDirectory.Finder.Configuration
{
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
}