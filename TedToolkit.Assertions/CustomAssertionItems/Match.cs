// -----------------------------------------------------------------------
// <copyright file="Match.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;

/// <summary>
/// Asserts that the subject satisfies the given predicate.
/// </summary>
/// <param name="predicate">The predicate the subject must satisfy.</param>
/// <param name="predicateName">The captured expression of the predicate (auto-filled by the source generator).</param>
/// <typeparam name="TSubject">The type of the subject.</typeparam>
internal readonly struct Match<TSubject>(
    Func<TSubject, bool> predicate,
    [AssertionParameterName(nameof(predicate))]
    string predicateName = "")
    : IAssertionItem<TSubject>
{
    /// <inheritdoc/>
    public bool IsPassed(TSubject subject)
    {
        return predicate(subject);
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.Match(predicateName));
    }
}