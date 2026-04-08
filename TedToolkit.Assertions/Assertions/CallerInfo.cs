// -----------------------------------------------------------------------
// <copyright file="CallerInfo.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Cysharp.Text;

namespace TedToolkit.Assertions;

/// <summary>
/// Captures the caller's location at the point where an assertion was created.
/// </summary>
/// <param name="MemberName">The name of the calling member.</param>
/// <param name="FilePath">The source file path of the caller.</param>
/// <param name="LineCount">The line number in the source file.</param>
public readonly record struct CallerInfo(
    string MemberName,
    string FilePath,
    int LineCount)
{
    /// <inheritdoc />
    public override string ToString()
    {
        using var builder = ZString.CreateStringBuilder();
        builder.Append("at ");
        builder.Append(MemberName);
        builder.Append(" in ");
        builder.Append(FilePath);
        builder.Append(":line ");
        builder.Append(LineCount);
        return builder.ToString();
    }
}