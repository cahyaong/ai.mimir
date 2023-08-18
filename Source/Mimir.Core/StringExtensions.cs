// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, August 17, 2023 4:48:54 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace System;

using System.Text.RegularExpressions;

public static partial class StringExtensions
{
    public static string Cleanse(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        return Pattern
            .ForReference()
            .Replace(text, string.Empty);
    }

    public static partial class Pattern
    {
        [GeneratedRegex(@"\[[\da-z]+?\]", RegexOptions.Compiled)]
        public static partial Regex ForReference();
    }
}