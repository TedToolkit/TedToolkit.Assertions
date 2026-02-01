// -----------------------------------------------------------------------
// <copyright file="Match.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions.CustomAssertionItems;

/// <summary>
/// Match item.
/// </summary>
/// <param name="predicate">predicate.</param>
/// <param name="predicateName">predicate name.</param>
/// <typeparam name="TSubject">the subject.</typeparam>
internal readonly struct Match<TSubject>(
    Func<TSubject, bool> predicate,
    [AssertionParameterName(nameof(predicate))]
    string predicateName = "")
    : IAssertionItem<TSubject>
{
    /// <inheritdoc/>
    public bool IsPassed(TSubject subject)
        => predicate(subject);

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.Match(predicateName));
}