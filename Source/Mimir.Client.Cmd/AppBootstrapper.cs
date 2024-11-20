// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, November 20, 2024 6:37:31 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

using nGratis.AI.Mimir.Core;

namespace nGratis.AI.Mimir.Client.Cmd;

using Autofac;

internal class AppBootstrapper : IDisposable
{
    private readonly IContainer _container = new ContainerBuilder()
        .RegisterStorage()
        .RegisterDataPipeline()
        .Build();

    private bool _isDisposed;

    ~AppBootstrapper()
    {
        this.Dispose(false);
    }

    public IDataFetcher CreateDataFetcher() => this._container.Resolve<IDataFetcher>();

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool isDisposing)
    {
        if (this._isDisposed)
        {
            return;
        }

        if (isDisposing)
        {
            this._container.Dispose();
        }

        this._isDisposed = true;
    }
}