// -----------------------------------------------------------------------
// <copyright file="BeLessThanOrEqualTo.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;
#pragma warning disable CA1815

/// <summary>
/// Be less than or equal to.
/// </summary>
/// <param name="comparedValue">expected value.</param>
/// <param name="comparer">the comparer.</param>
/// <typeparam name="TSubject">type.</typeparam>
public readonly struct BeLessThanOrEqualTo<TSubject>(
    TSubject comparedValue,
    IComparer<TSubject>? comparer = null) : IAssertionItem<TSubject>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var realComparer = comparer ?? Comparer<TSubject>.Default;
        return realComparer.Compare(subject, comparedValue) <= 0;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeLessThanOrEqualTo(
            AssertionHelpers.GetObjectString(comparedValue)));
    }
}