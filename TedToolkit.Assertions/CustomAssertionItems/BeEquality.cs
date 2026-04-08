// -----------------------------------------------------------------------
// <copyright file="BeEquality.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;
#pragma warning disable CA1815

/// <summary>
/// Asserts that the subject equals the expected value using an <see cref="IEqualityComparer{T}"/>.
/// </summary>
/// <param name="expectedValue">The expected value.</param>
/// <param name="equalityComparer">An optional equality comparer; defaults to <see cref="EqualityComparer{T}.Default"/>.</param>
/// <param name="expectedValueName">The captured expression of the expected value (auto-filled by the source generator).</param>
/// <typeparam name="TSubject">The type of the subject.</typeparam>
[AssertionMethodPriority(1)]
[AssertionMethodName("Be")]
[AssertionMethodName("BeEqualTo")]
internal readonly struct BeEquality<TSubject>(
    TSubject expectedValue,
    IEqualityComparer<TSubject>? equalityComparer = null,
    [AssertionParameterName(nameof(expectedValue))]
    string expectedValueName = "") : IAssertionItem<TSubject>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var comparer = equalityComparer ?? EqualityComparer<TSubject>.Default;
        return comparer.Equals(subject, expectedValue);
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.Be(
            expectedValueName, AssertionHelpers.GetObjectString(expectedValue)));
    }
}