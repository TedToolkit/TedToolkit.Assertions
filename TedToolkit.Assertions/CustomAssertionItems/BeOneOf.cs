// -----------------------------------------------------------------------
// <copyright file="BeOneOf.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// be one of.
/// </summary>
/// <param name="expectedValues">values.</param>
/// <param name="equalityComparer">equality comparer.</param>
/// <typeparam name="TSubject">the type.</typeparam>
internal readonly struct BeOneOf<TSubject>(
    IReadOnlyCollection<TSubject> expectedValues,
    IEqualityComparer<TSubject>? equalityComparer = null) : IAssertionItem<TSubject>
{
    /// <inheritdoc/>
    public bool IsPassed(TSubject subject)
        => expectedValues.Contains(subject, equalityComparer ?? EqualityComparer<TSubject>.Default);

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeOneOf(
            AssertionHelpers.GetObjectsString(expectedValues)));
    }
}