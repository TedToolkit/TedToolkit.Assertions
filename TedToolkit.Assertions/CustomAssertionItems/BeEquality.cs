// -----------------------------------------------------------------------
// <copyright file="BeEquality.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions.CustomAssertionItems;
#pragma warning disable CA1815

/// <summary>
/// Be.
/// </summary>
/// <param name="expectedValue">expected value.</param>
/// <param name="equalityComparer">comparer.</param>
/// <typeparam name="TSubject">the type.</typeparam>
[AssertionMethodPriority(1)]
[AssertionMethodName("Be")]
internal readonly struct BeEquality<TSubject>(
    TSubject expectedValue,
    IEqualityComparer<TSubject>? equalityComparer = null) : IAssertionItem<TSubject>
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
            AssertionHelpers.GetObjectString(expectedValue)));
    }
}