// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AweArticleVisualizer.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, August 22, 2023 5:37:45 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Client.Maui;

using nGratis.AI.Mimir.Core;

public class AweArticleVisualizer : ContentView
{
    public static readonly BindableProperty ArticleProperty = BindableProperty.Create(
        nameof(AweArticleVisualizer.Article),
        typeof(Article),
        typeof(AweArticleVisualizer),
        Article.Empty,
        propertyChanged: OnArticleChanged);

    public static readonly BindableProperty FormattedTextProperty = BindableProperty.Create(
        nameof(AweArticleVisualizer.FormattedText),
        typeof(FormattedString),
        typeof(AweArticleVisualizer),
        new FormattedString());

    public Article Article
    {
        get => (Article)this.GetValue(AweArticleVisualizer.ArticleProperty);
        set => this.SetValue(AweArticleVisualizer.ArticleProperty, value);
    }

    public FormattedString FormattedText
    {
        get => (FormattedString)this.GetValue(AweArticleVisualizer.FormattedTextProperty);
        private set => this.SetValue(AweArticleVisualizer.FormattedTextProperty, value);
    }

    private static void OnArticleChanged(BindableObject bindable, object _, object newValue)
    {
        if (bindable is not AweArticleVisualizer visualizer)
        {
            return;
        }

        if (newValue is not Article article)
        {
            return;
        }

        // TODO (MUST): Execute conversion in background threads, and consider pagination?

        var formattedText = new FormattedString();

        if (article == Article.Empty)
        {
            visualizer.FormattedText = formattedText;
            return;
        }

        var breakline = new Span
        {
            Text = $"{Environment.NewLine}{Environment.NewLine}",
            FontSize = 14,
            LineHeight = 20
        };

        foreach (var section in article.Sections)
        {
            formattedText.Spans.Add(new Span
            {
                Text = section.Header,
                FontSize = 28,
                LineHeight = 36
            });

            formattedText.Spans.Add(breakline);

            foreach (var paragraph in section.Paragraphs)
            {
                formattedText.Spans.Add(new Span
                {
                    Text = paragraph,
                    FontSize = 14,
                    LineHeight = 20
                });

                formattedText.Spans.Add(breakline);
            }
        }

        formattedText.Spans.RemoveAt(formattedText.Spans.Count - 1);

        visualizer.FormattedText = formattedText;
    }
}