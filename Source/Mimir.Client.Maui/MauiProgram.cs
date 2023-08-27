// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MauiProgram.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Sunday, July 30, 2023 12:26:31 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Client.Maui;

using System.Runtime.Versioning;
using Autofac.Extensions.DependencyInjection;
using CommunityToolkit.Maui;

[SupportedOSPlatform("windows")]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        return MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .UseWindowsFont()
            .UseMauiCommunityToolkit()
            .UseAutofacInjection()
            .Build();
    }

    private static MauiAppBuilder UseAutofacInjection(this MauiAppBuilder appBuilder)
    {
        appBuilder.ConfigureContainer(
            new AutofacServiceProviderFactory(),
            containerBuilder => containerBuilder
                .RegisterViewModel()
                .RegisterStorage()
                .RegisterDataPipeline());

        return appBuilder;
    }

    private static MauiAppBuilder UseWindowsFont(this MauiAppBuilder appBuilder)
    {
        return appBuilder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSans-Regular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSans-Semibold");
        });
    }
}