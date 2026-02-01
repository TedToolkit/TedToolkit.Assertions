// -----------------------------------------------------------------------
// <copyright file="ContainPredicate.cs" company="TedToolkit">
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
/// <param name="predicate">predicate.</param>
/// <param name="predicateName">predicate name.</param>
/// <typeparam name="TSubject">the type of the subject.</typeparam>
/// <typeparam name="TItem">the item.</typeparam>
[AssertionMethodName("Contain")]
internal readonly struct ContainPredicate<TSubject, TItem>(
    Func<TItem, bool> predicate,
    [AssertionParameterName(nameof(predicate))]
    string predicateName = "") : IAssertionItem<TSubject>
    where TSubject : IReadOnlyCollection<TItem>
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
        => subject.Any(predicate);

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(
            Localization.ExpectedStatements.ContainPredicate(predicateName),
            Localization.ActualStatements.ThereAre(AssertionHelpers.GetObjectsString(assertion.Info.Subject)));
    }
}