// -----------------------------------------------------------------------
// <copyright file="BeInRange.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;

/// <summary>
/// Asserts that the subject falls within the inclusive range [<paramref name="minimumValue"/>, <paramref name="maximumValue"/>].
/// </summary>
/// <param name="minimumValue">The lower bound (inclusive).</param>
/// <param name="maximumValue">The upper bound (inclusive).</param>
/// <param name="comparer">An optional comparer; defaults to <see cref="Comparer{T}.Default"/>.</param>
/// <param name="minimumValueName">The captured expression of the minimum value (auto-filled by the source generator).</param>
/// <param name="maximumValueName">The captured expression of the maximum value (auto-filled by the source generator).</param>
/// <typeparam name="TSubject">The type of the subject.</typeparam>
internal readonly struct BeInRange<TSubject>(
    TSubject minimumValue,
    TSubject maximumValue,
    IComparer<TSubject>? comparer = null,
    [AssertionParameterName(nameof(minimumValue))]
    string minimumValueName = "",
    [AssertionParameterName(nameof(maximumValue))]
    string maximumValueName = "") : IAssertionItem<TSubject>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var realComparer = comparer ?? Comparer<TSubject>.Default;
        return realComparer.Compare(subject, minimumValue) >= 0 && realComparer.Compare(subject, maximumValue) <= 0;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements
            .BeInRange(
                minimumValueName, AssertionHelpers.GetObjectString(minimumValue),
                maximumValueName, AssertionHelpers.GetObjectString(maximumValue)));
    }
}