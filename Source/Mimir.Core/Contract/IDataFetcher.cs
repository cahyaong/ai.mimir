// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataFetcher.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, August 12, 2023 5:54:22 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Core;

public interface IDataFetcher : IDisposable
{
    Task<Article> FetchArticleAsync(string keyword);
}