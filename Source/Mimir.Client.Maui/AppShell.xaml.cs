// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppShell.xaml.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Sunday, July 30, 2023 12:26:31 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Client.Maui;

using System.Collections.Immutable;
using System.Reflection;

public partial class AppShell
{
    private static readonly IDictionary<string, Type> RegisteredTypeByNameLookup;

    private readonly IServiceProvider _serviceProvider;

    static AppShell()
    {
        AppShell.RegisteredTypeByNameLookup = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type is { IsAbstract: false, IsInterface: false })
            .Where(type => type.Name?.EndsWith(Keyword.ViewModel) == true)
            .ToImmutableDictionary(type => type.Name);
    }

    public AppShell(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;

        this.InitializeComponent();
    }

    protected override void OnNavigated(ShellNavigatedEventArgs _)
    {
        if (Application.Current?.MainPage is not Shell shell)
        {
            return;
        }

        var id = shell
            .CurrentPage.GetType().Name
            .Replace(Keyword.Page, Keyword.ViewModel);

        if (AppShell.RegisteredTypeByNameLookup.TryGetValue(id, out var type))
        {
            shell.CurrentPage.BindingContext = this._serviceProvider.GetService(type);
        }
    }

    private static class Keyword
    {
        public const string Page = "Page";

        public const string ViewModel = "ViewModel";
    }
}