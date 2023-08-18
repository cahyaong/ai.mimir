// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Section.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, August 17, 2023 3:53:38 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Core;

using System.Collections.Immutable;
using nGratis.Cop.Olympus.Contract;

public record Section
{
    private Section()
    {
        this.Header = DefinedText.Unknown;
        this.Paragraphs = ImmutableArray<string>.Empty;
    }

    public string Header { get; private init; }

    public IEnumerable<string> Paragraphs { get; private init; }

    public class Builder
    {
        private string _header;
        private readonly IList<string> _paragraphs;

        private Builder()
        {
            this._header = DefinedText.Unknown;
            this._paragraphs = new List<string>();
        }

        public static Builder Create() => new();

        public Builder WithHeader(string header)
        {
            Guard
                .Require(header, nameof(header))
                .Is.Not.Empty();

            this._header = header;

            return this;
        }

        public Builder WithParagraph(string paragraph)
        {
            Guard
                .Require(paragraph, nameof(paragraph))
                .Is.Not.Empty();

            this._paragraphs.Add(paragraph
                .Cleanse()
                .Trim());

            return this;
        }

        public Section Build() => new()
        {
            Header = this._header,
            Paragraphs = this._paragraphs
        };
    }
}