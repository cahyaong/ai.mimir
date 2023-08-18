// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WikipediaFetcher.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, August 16, 2023 5:13:42 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Core;

using Humanizer;
using nGratis.Cop.Olympus.Contract;

public partial class WikipediaFetcher : HttpFetcherBase
{
    public WikipediaFetcher(IStorageManager storageManager)
        : base("WIKIPEDIA", storageManager, KeyCalculation.Instance)
    {
    }

    public override async Task<Article> FetchArticleAsync(string keyword)
    {
        Guard
            .Require(keyword, nameof(keyword))
            .Is.Not.Empty();

        var document = await this.FetchAsync(
            new Uri(Link.LandingUri,
            $"wiki/{keyword.Humanize(LetterCasing.Title).Replace(" ", "_")}"));

        var elements = document
            .QuerySelectorAll("#mw-content-text .mw-parser-output :is(h2,p)")
            .Where(element => !element.ClassList.Contains("mw-empty-elt"))
            .ToList();

        var articleBuilder = Article.Builder
            .Create()
            .WithTitle(keyword.Humanize(LetterCasing.Title));

        var sectionBuilder = Section.Builder
            .Create()
            .WithHeader(DefinedText.Empty);

        foreach (var element in elements)
        {
            switch (element.TagName)
            {
                case "H2":
                    articleBuilder
                        .WithSection(sectionBuilder.Build());

                    sectionBuilder = Section.Builder
                        .Create()
                        .WithHeader(element.TextContent);

                    break;

                case "P":
                    sectionBuilder
                        .WithParagraph(element.TextContent);

                    break;

                default:
                    throw new MimirException("Element tag must be handled!", ("Name", element.TagName));
            }
        }

        return articleBuilder.Build();
    }

    private static class Link
    {
        public static readonly Uri LandingUri = new("https://en.wikipedia.org");
    }
}