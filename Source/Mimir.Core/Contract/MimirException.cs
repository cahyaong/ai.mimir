// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MimirException.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, August 12, 2023 6:14:42 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.AI.Mimir.Core;

public class MimirException : Exception
{
    public MimirException(string message)
        : base(message)
    {
    }

    public MimirException(string message, params string[] submessages)
        : base(submessages.Aggregate(message, (blob, submessage) => $"{blob} {submessage}"))
    {
    }

    public MimirException(string message, params (string Key, object Value)[] details)
        : this(message, details.Select(detail => $"{detail.Key}: [{detail.Value}].").ToArray())
    {
    }
}