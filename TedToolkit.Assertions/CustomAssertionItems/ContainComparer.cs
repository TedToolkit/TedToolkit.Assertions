// -----------------------------------------------------------------------
// <copyright file="ContainComparer.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions.CustomAssertionItems;
#pragma warning disable CA1815

/// <summary>
/// Contain single.
/// </summary>
/// <param name="expectedValue">expected value.</param>
/// <param name="comparer"> comparer.</param>
/// <typeparam name="TSubject">the type of the subject.</typeparam>
/// <typeparam name="TItem">the item.</typeparam>
[AssertionMethodName("Contain")]
internal readonly struct ContainComparer<TSubject, TItem>(
    TItem expectedValue,
    IComparer<TItem>? comparer = null) : IAssertionItem<TSubject>
    where TSubject : IReadOnlyCollection<TItem>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var realComparer = comparer ?? Comparer<TItem>.Default;
        foreach (var item in subject)
        {
            if (realComparer.Compare(item, expectedValue) is 0)
                return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(
            Localization.ExpectedStatements.ContainItem(AssertionHelpers.GetObjectString(expectedValue)),
            Localization.ActualStatements.ThereAre(AssertionHelpers.GetObjectsString(assertion.Info.Subject)));
    }
}