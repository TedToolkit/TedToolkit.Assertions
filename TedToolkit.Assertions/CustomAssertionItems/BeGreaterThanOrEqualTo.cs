// -----------------------------------------------------------------------
// <copyright file="BeGreaterThanOrEqualTo.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;
#pragma warning disable CA1815

/// <summary>
/// Asserts that the subject is greater than or equal to the specified value.
/// </summary>
/// <param name="comparedValue">The lower bound (inclusive).</param>
/// <param name="comparer">An optional comparer; defaults to <see cref="Comparer{T}.Default"/>.</param>
/// <param name="comparedValueName">The captured expression of the compared value (auto-filled by the source generator).</param>
/// <typeparam name="TSubject">The type of the subject.</typeparam>
public readonly struct BeGreaterThanOrEqualTo<TSubject>(
    TSubject comparedValue,
    IComparer<TSubject>? comparer = null,
    [AssertionParameterName(nameof(comparedValue))]
    string comparedValueName = "") : IAssertionItem<TSubject>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var realComparer = comparer ?? Comparer<TSubject>.Default;
        return realComparer.Compare(subject, comparedValue) >= 0;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeGreaterThanOrEqualTo(
            comparedValueName, AssertionHelpers.GetObjectString(comparedValue)));
    }
}