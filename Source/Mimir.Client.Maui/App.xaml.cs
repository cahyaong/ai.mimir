// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Sunday, July 30, 2023 12:26:31 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

using nGratis.AI.Mimir.Core;

namespace nGratis.AI.Mimir.Client.Maui;

public partial class App
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;

        this.InitializeComponent();

        this.MainPage = new AppShell(serviceProvider);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Width = 1392;
        window.Height = 1044;

        window.Destroying += this.OnWindowDestroying;

        return window;
    }

    private void OnWindowDestroying(object? _, EventArgs __)
    {
        this._serviceProvider
            .GetServices<IDataFetcher>()
            .OfType<IDisposable>()
            .ForEach(disposable => disposable.Dispose());
    }
}