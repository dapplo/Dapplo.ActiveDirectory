// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Autofac;
using Dapplo.ActiveDirectory.Finder.Configuration;
using Dapplo.ActiveDirectory.Finder.Ui.ViewModels;
using Dapplo.Addons;
using Dapplo.CaliburnMicro;
using Dapplo.CaliburnMicro.Configuration;
using Dapplo.CaliburnMicro.Menu;
using Dapplo.Config.Language;

namespace Dapplo.ActiveDirectory.Finder;

/// <inheritdoc />
public class FinderAddonModule : AddonModule
{
    /// <inheritdoc />
    protected override void Load(ContainerBuilder builder)
    {
        // All IMenuItem with the context they belong to
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AssignableTo<IMenuItem>()
            .As<IMenuItem>()
            .SingleInstance();

        builder
            .Register(c => Language<IFinderTranslations>.Create())
            .As<ILanguage>()
            .As<IFinderTranslations>()
            .SingleInstance();

/*

            builder
                .RegisterType<LogConfigurationImpl>()
                .As<IIniSection>()
                .As<ILogConfiguration>()
                .SingleInstance();*/


        // All config screens
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AssignableTo<IConfigScreen>()
            .As<IConfigScreen>()
            .SingleInstance();

/*
            
            builder
                .RegisterType<SystemTrayContextMenuViewModel>()
                .As<ITrayIconViewModel>()
                .WithAttributeFiltering()
                .SingleInstance();*/

        builder
            .RegisterType<FinderViewModel>()
            .As<IShell>()
            .AsSelf()
            .SingleInstance();
    }
}