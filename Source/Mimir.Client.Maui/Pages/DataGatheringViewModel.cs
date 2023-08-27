// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataGatheringViewModel.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, August 1, 2023 5:52:58 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Client.Maui;

using System.Reactive;
using nGratis.AI.Mimir.Core;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

internal class DataGatheringViewModel : ReactiveObject
{
    private readonly IDataFetcher _dataFetcher;

    public DataGatheringViewModel(IDataFetcher dataFetcher)
    {
        this._dataFetcher = dataFetcher;

        this.Query = string.Empty;
        this.IsFetching = false;
        this.Article = Article.Empty;

        this.FetchingContentCommand = ReactiveCommand.CreateFromTask(
            this.FetchContentAsync,
            this.WhenAnyValue(
                local => local.IsFetching,
                local => local.Query,
                (isFetching, query) => !isFetching && !string.IsNullOrEmpty(query)));

        Catalyst.Models.English.Register();
    }

    [Reactive]
    public string Query { get; set; }

    [Reactive]
    public bool IsFetching { get; private set; }

    [Reactive]
    public Article Article { get; private set; }

    public ReactiveCommand<Unit, Unit> FetchingContentCommand { get; }

    private async Task FetchContentAsync()
    {
        this.IsFetching = true;

        try
        {
            this.Article = await this._dataFetcher.FetchArticleAsync(this.Query);
        }
        catch (MimirException)
        {
            this.Article = Article.Empty;
        }

        this.IsFetching = false;
    }
}