// -----------------------------------------------------------------------
// <copyright file="SubjectInfo.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Cysharp.Text;

namespace TedToolkit.Assertions;

/// <summary>
/// The info of the subject, which may be record.
/// </summary>
/// <param name="SubjectName">subject name.</param>
/// <param name="MemberName">the member name.</param>
/// <param name="FilePath">file path.</param>
/// <param name="LineCount">line count.</param>
/// <param name="CreatedAt">created time.</param>
public readonly record struct SubjectInfo(
    string SubjectName,
    string MemberName,
    string FilePath,
    int LineCount,
    DateTimeOffset CreatedAt)
{
    /// <summary>
    /// Changed the name of the subject info.
    /// </summary>
    /// <param name="operation">operation name.</param>
    /// <returns>result.</returns>
    public SubjectInfo SubOperation(string operation)
        => this with { SubjectName = ZString.Concat(SubjectName, " -> ", operation) };
}