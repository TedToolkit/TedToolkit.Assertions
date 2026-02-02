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
/// Be.
/// </summary>
/// <param name="expectedValue">expected value.</param>
/// <param name="equalityComparer">the comparer.</param>
/// <typeparam name="TSubject">type.</typeparam>
[AssertionMethodName("Be")]
internal readonly struct BeComparer<TSubject>(
    TSubject expectedValue,
    IComparer<TSubject>? equalityComparer = null) : IAssertionItem<TSubject>
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
            AssertionHelpers.GetObjectString(expectedValue)));
    }
}