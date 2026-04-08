// -----------------------------------------------------------------------
// <copyright file="BeComparer.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;
#pragma warning disable CA1815

/// <summary>
/// Asserts that the subject equals the expected value using an <see cref="IComparer{T}"/> (comparison result of zero).
/// </summary>
/// <param name="expectedValue">The expected value.</param>
/// <param name="equalityComparer">An optional comparer; defaults to <see cref="Comparer{T}.Default"/>.</param>
/// <param name="expectedValueName">The captured expression of the expected value (auto-filled by the source generator).</param>
/// <typeparam name="TSubject">The type of the subject.</typeparam>
[AssertionMethodName("Be")]
[AssertionMethodName("BeEqualTo")]
internal readonly struct BeComparer<TSubject>(
    TSubject expectedValue,
    IComparer<TSubject>? equalityComparer = null,
    [AssertionParameterName(nameof(expectedValue))]
    string expectedValueName = "") : IAssertionItem<TSubject>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var comparer = equalityComparer ?? Comparer<TSubject>.Default;
        return comparer.Compare(subject, expectedValue) is 0;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.Be(
            expectedValueName, AssertionHelpers.GetObjectString(expectedValue)));
    }
}