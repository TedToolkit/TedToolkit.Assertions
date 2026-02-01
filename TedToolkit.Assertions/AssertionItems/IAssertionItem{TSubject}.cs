// -----------------------------------------------------------------------
// <copyright file="IAssertionItem{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// The assertion item. make this type as <b>internal readonly struct</b>.
/// </summary>
/// <typeparam name="TSubject">the subject.</typeparam>
#pragma warning disable S3246
public interface IAssertionItem<TSubject>
#pragma warning restore S3246
{
    /// <summary>
    /// Is passed this assertion.
    /// </summary>
    /// <param name="subject">the subject.</param>
    /// <returns>is passed.</returns>
    bool IsPassed(TSubject subject);

    /// <summary>
    /// Generate the failed message.
    /// </summary>
    /// <param name="assertion">assertion item.</param>
    /// <returns>message.</returns>
    string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion);
}