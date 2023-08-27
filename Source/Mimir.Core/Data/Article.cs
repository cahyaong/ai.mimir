// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Article.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, August 12, 2023 5:38:10 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Core;

using System.Collections.Immutable;
using nGratis.Cop.Olympus.Contract;

public record Article
{
    private Article()
    {
        this.Title = DefinedText.Unknown;
        this.Sections = ImmutableArray<Section>.Empty;
    }

    public static Article Empty { get; } = new()
    {
        Title = DefinedText.Empty,
        Sections = ImmutableArray<Section>.Empty
    };

    public string Title { get; private init; }

    public IEnumerable<Section> Sections { get; private init; }

    public class Builder
    {
        private string _title;
        private readonly IList<Section> _sections;

        private Builder()
        {
            this._title = DefinedText.Unknown;
            this._sections = new List<Section>();
        }

        public static Builder Create() => new();

        public Builder WithTitle(string title)
        {
            Guard
                .Require(title, nameof(title))
                .Is.Not.Empty();

            this._title = title;

            return this;
        }

        public Builder WithSection(Section section)
        {
            if (section.Paragraphs.Any())
            {
                this._sections.Add(section);
            }

            return this;
        }

        public Article Build() => new()
        {
            Title = this._title,
            Sections = this._sections
        };
    }
}