// -----------------------------------------------------------------------
// <copyright file="SubjectInfo.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Cysharp.Text;

namespace TedToolkit.Assertions;

/// <summary>
/// Metadata that describes a subject: its expression name, the caller's location, and the time the assertion was created.
/// </summary>
/// <param name="SubjectName">The expression name of the subject (captured via <c>CallerArgumentExpression</c>).</param>
/// <param name="CallerInfo">The caller's source location.</param>
/// <param name="CreatedAt">The timestamp when the assertion was created.</param>
public readonly record struct SubjectInfo(
    string SubjectName,
    CallerInfo CallerInfo,
    DateTimeOffset CreatedAt)
{
    /// <summary>
    /// Creates a derived <see cref="SubjectInfo"/> whose name appends a sub-operation suffix (e.g. <c>items -&gt; SingleBy</c>).
    /// </summary>
    /// <param name="operation">The sub-operation name to append.</param>
    /// <returns>A new <see cref="SubjectInfo"/> with the extended name.</returns>
    public SubjectInfo SubOperation(string operation)
    {
        return this with { SubjectName = ZString.Concat(SubjectName, " -> ", operation) };
    }
}