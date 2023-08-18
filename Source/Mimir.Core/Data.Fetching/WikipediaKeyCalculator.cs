// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WikipediaKeyCalculator.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, August 17, 2023 4:37:04 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Core;

using System.Text.RegularExpressions;
using Humanizer;
using nGratis.Cop.Olympus.Contract;

public partial class WikipediaFetcher
{
    private class KeyCalculation : IKeyCalculator
    {
        private KeyCalculation()
        {
        }

        public static KeyCalculation Instance { get; } = new();

        public DataSpec Calculate(Uri uri)
        {
            var urlMatch = Pattern.ForArticleUrl().Match(uri.PathAndQuery);

            if (urlMatch.Success)
            {
                return new DataSpec(
                    $"Article_{urlMatch.Groups["id"].Value.Dehumanize()}",
                    Mime.Html);
            }

            throw new MimirException("Key calculation must be handled!", ("URL", uri.AbsoluteUri));
        }
    }

    private static partial class Pattern
    {
        [GeneratedRegex(@"/wiki/(?<id>\w+)", RegexOptions.Compiled)]
        public static partial Regex ForArticleUrl();
    }
}