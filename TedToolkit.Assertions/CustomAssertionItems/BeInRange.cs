// -----------------------------------------------------------------------
// <copyright file="BeInRange.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.CustomAssertionItems;

/// <summary>
/// Should be in range.
/// </summary>
/// <param name="minimumValue">min value.</param>
/// <param name="maximumValue">max value.</param>
/// <param name="comparer">comparer.</param>
/// <typeparam name="TSubject">type.</typeparam>
internal readonly struct BeInRange<TSubject>(
    TSubject minimumValue,
    TSubject maximumValue,
    IComparer<TSubject>? comparer = null) : IAssertionItem<TSubject>
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
                AssertionHelpers.GetObjectString(minimumValue),
                AssertionHelpers.GetObjectString(maximumValue)));
    }
}