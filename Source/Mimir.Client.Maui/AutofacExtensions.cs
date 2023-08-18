// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutofacExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, August 17, 2023 6:44:53 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Client.Maui;

using System.Reflection;
using Autofac;
using nGratis.AI.Mimir.Core;
using nGratis.Cop.Olympus.Contract;
using nGratis.Cop.Olympus.Framework;

public static class AutofacExtensions
{
    public static ContainerBuilder RegisterStorage(this ContainerBuilder containerBuilder)
    {
        var dataFolderUri = AutofacExtensions.FindDataFolderUri();

        containerBuilder
            .Register(_ => new FileStorageManager(dataFolderUri))
            .InstancePerLifetimeScope()
            .As<IStorageManager>();

        return containerBuilder;
    }

    public static ContainerBuilder RegisterDataPipeline(this ContainerBuilder containerBuilder)
    {
        containerBuilder
            .RegisterType<WikipediaFetcher>()
            .InstancePerLifetimeScope()
            .As<IDataFetcher>();

        return containerBuilder;
    }

    public static ContainerBuilder RegisterViewModel(this ContainerBuilder containerBuilder)
    {
        containerBuilder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(type => type is { IsAbstract: false, IsInterface: false })
            .Where(type => type.Name?.EndsWith("ViewModel") == true)
            .InstancePerLifetimeScope();

        return containerBuilder;
    }

    private static Uri FindDataFolderUri()
    {
        var dataFolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NGRATIS",
            "ai.mimir");

        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }

        return new Uri(dataFolderPath);
    }
}