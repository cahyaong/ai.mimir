// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Friday, November 15, 2024 4:28:18 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Client.Cmd;

using System.Diagnostics;

public class Program
{
    private static async Task Main(string[] args)
    {
        using var appBootstrapper = new AppBootstrapper();

        var dataFetcher = appBootstrapper.CreateDataFetcher();
        var article = await dataFetcher.FetchArticleAsync("Ancient Greece");

        if (Debugger.IsAttached)
        {
            Console.WriteLine("Press <ENTER> key to continue...");
            Console.ReadLine();
        }
    }
}