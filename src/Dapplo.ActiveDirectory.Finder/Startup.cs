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

using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using Dapplo.Addons.Bootstrapper;
using Dapplo.CaliburnMicro.Dapp;
using Dapplo.Config.Ini.Converters;
using Dapplo.Log;
#if DEBUG
using Dapplo.Log.Loggers;
#else
using Dapplo.Log.LogFile;
#endif

namespace Dapplo.ActiveDirectory.Finder
{
    /// <summary>
    ///     This takes care or starting the Application
    /// </summary>
    public static class Startup
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        ///     Start the application
        /// </summary>
        [STAThread]
        public static int Main()
        {
#if DEBUG
            // Initialize a debug logger for Dapplo packages
            LogSettings.RegisterDefaultLogger<DebugLogger>(LogLevels.Verbose);
#else
            LogSettings.RegisterDefaultLogger<ForwardingLogger>(LogLevels.Debug);
#endif

            // TODO: Set via build
            StringEncryptionTypeConverter.RgbIv = "dlgjowejgogkklwj";
            StringEncryptionTypeConverter.RgbKey = "lsjvkwhvwujkagfauguwcsjgu2wueuff";

            // Use this to setup the culture of your UI
            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            var applicationConfig = ApplicationConfigBuilder
                .Create()
                .WithApplicationName("Finder")
                .WithMutex("896482B0-2E32-480C-A9C1-87B1CCF245BF")
                .WithoutCopyOfEmbeddedAssemblies()
                .WithCaliburnMicro()
                .BuildApplicationConfig();

            var application = new Dapplication(applicationConfig)
            {
                ShutdownMode = ShutdownMode.OnLastWindowClose
            };

            // Prevent multiple instances
            if (application.WasAlreadyRunning)
            {
                Log.Warn().WriteLine("{0} was already running.", applicationConfig.ApplicationName);
                // Don't start the dapplication, exit with 0
                application.Shutdown(-1);
                return -1;
            }

            RegisterErrorHandlers(application);

            application.Run();
            return 0;
        }

        /// <summary>
        /// Make sure all exception handlers are hooked
        /// </summary>
        /// <param name="application">Dapplication</param>
        private static void RegisterErrorHandlers(Dapplication application)
        {
            application.OnUnhandledAppDomainException += (exception, b) => DisplayErrorViewModel(exception);
            application.OnUnhandledDispatcherException += DisplayErrorViewModel;
            application.OnUnhandledTaskException += DisplayErrorViewModel;
        }

        /// <summary>
        /// Show the exception
        /// </summary>
        /// <param name="exception">Exception</param>
        private static void DisplayErrorViewModel(Exception exception)
        {
            MessageBox.Show(exception.ToString());
        }
    }
}