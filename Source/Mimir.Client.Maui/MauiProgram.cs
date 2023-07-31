// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MauiProgram.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Sunday, July 30, 2023 12:26:31 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Client.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
#pragma warning disable CA1416
        return MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .Build();
#pragma warning restore CA1416
    }
}