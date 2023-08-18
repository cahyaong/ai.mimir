// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpFetcherBase.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, August 12, 2023 5:55:19 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Core;

using System.Net.Http.Headers;
using AngleSharp;
using AngleSharp.Dom;
using nGratis.Cop.Olympus.Contract;
using nGratis.Cop.Olympus.Framework;

public abstract class HttpFetcherBase : IDataFetcher
{
    private readonly HttpClient _httpClient;

    private bool _isDisposed;

    protected HttpFetcherBase(string id, IStorageManager storageManager, IKeyCalculator keyCalculator)
    {
        var messageHandler = (HttpMessageHandler)new HttpClientHandler();
        messageHandler = new ThrottlingMessageHandler(TimeSpan.FromMilliseconds(25), messageHandler);
        messageHandler = new CachingMessageHandler($"Raw_{id}", storageManager, keyCalculator, messageHandler);

        this._httpClient = new HttpClient(messageHandler);
        this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
        this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
        this._httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        this._httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AI.Mimir", "0.1"));
    }

    public abstract Task<Article> FetchArticleAsync(string keyword);

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected async Task<IDocument> FetchAsync(Uri uri)
    {
        var response = await this._httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            throw new MimirException(
                "Fetching content could not complete successfully!",
                ("URL", uri.AbsoluteUri),
                ("HTTP Code", response.StatusCode));
        }

        var content = await response.Content.ReadAsStringAsync();

        return await BrowsingContext
            .New(Configuration.Default)
            .OpenAsync(request => request.Content(content));
    }

    private void Dispose(bool isDisposing)
    {
        if (this._isDisposed)
        {
            return;
        }

        if (isDisposing)
        {
            this._httpClient.Dispose();
        }

        this._isDisposed = true;
    }
}