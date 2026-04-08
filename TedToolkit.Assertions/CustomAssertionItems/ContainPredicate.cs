// -----------------------------------------------------------------------
// <copyright file="ContainPredicate.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;
#pragma warning disable CA1815

/// <summary>
/// Asserts that the collection contains at least one item matching the predicate.
/// </summary>
/// <param name="predicate">The predicate to match against.</param>
/// <param name="predicateName">The captured expression of the predicate (auto-filled by the source generator).</param>
/// <typeparam name="TSubject">The collection type.</typeparam>
/// <typeparam name="TItem">The element type.</typeparam>
[AssertionMethodName("Contain")]
internal readonly struct ContainPredicate<TSubject, TItem>(
    Func<TItem, bool> predicate,
    [AssertionParameterName(nameof(predicate))]
    string predicateName = "") : IAssertionItem<TSubject>
    where TSubject : IReadOnlyCollection<TItem>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        return subject.Any(predicate);
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(
            Localization.ExpectedStatements.ContainPredicate(predicateName),
            Localization.ActualStatements.ThereAre(AssertionHelpers.GetObjectsString(assertion.Info.Subject)));
    }
}