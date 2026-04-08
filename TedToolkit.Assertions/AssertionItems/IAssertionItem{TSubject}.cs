// -----------------------------------------------------------------------
// <copyright file="IAssertionItem{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Defines a single assertion check. Implementors should be <b>internal readonly struct</b>s so the source generator can discover them.
/// </summary>
/// <typeparam name="TSubject">The type of the subject being asserted.</typeparam>
#pragma warning disable S3246
public interface IAssertionItem<TSubject>
#pragma warning restore S3246
{
    /// <summary>
    /// Evaluates whether the <paramref name="subject"/> satisfies this assertion.
    /// </summary>
    /// <param name="subject">The subject value to check.</param>
    /// <returns><see langword="true"/> if the assertion passes; otherwise <see langword="false"/>.</returns>
    bool IsPassed(TSubject subject);

    /// <summary>
    /// Generates the failure message shown when this assertion does not pass.
    /// </summary>
    /// <param name="assertion">The assertion context (subject info, severity, inversion state).</param>
    /// <returns>The formatted failure message.</returns>
    string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion);
}