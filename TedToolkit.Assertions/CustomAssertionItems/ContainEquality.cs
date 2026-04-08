// -----------------------------------------------------------------------
// <copyright file="ContainEquality.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;
#pragma warning disable CA1815

/// <summary>
/// Asserts that the collection contains an item equal to the expected value, using an <see cref="IEqualityComparer{T}"/>.
/// </summary>
/// <param name="expectedValue">The value to search for.</param>
/// <param name="equalityComparer">An optional equality comparer; defaults to <see cref="EqualityComparer{T}.Default"/>.</param>
/// <param name="expectedValueName">The captured expression of the expected value (auto-filled by the source generator).</param>
/// <typeparam name="TSubject">The collection type.</typeparam>
/// <typeparam name="TItem">The element type.</typeparam>
[AssertionMethodName("Contain")]
[AssertionMethodPriority(1)]
internal readonly struct ContainEquality<TSubject, TItem>(
    TItem expectedValue,
    IEqualityComparer<TItem>? equalityComparer = null,
    [AssertionParameterName(nameof(expectedValue))]
    string expectedValueName = "") : IAssertionItem<TSubject>
    where TSubject : IReadOnlyCollection<TItem>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var comparer = equalityComparer ?? EqualityComparer<TItem>.Default;
        foreach (var item in subject)
        {
            if (comparer.Equals(item, expectedValue))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(
            Localization.ExpectedStatements.ContainItem(expectedValueName, AssertionHelpers.GetObjectString(expectedValue)),
            Localization.ActualStatements.ThereAre(AssertionHelpers.GetObjectsString(assertion.Info.Subject)));
    }
}