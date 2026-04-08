// -----------------------------------------------------------------------
// <copyright file="BeOneOf.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Asserts that the subject equals one of the specified values.
/// </summary>
/// <param name="expectedValues">The collection of allowed values.</param>
/// <param name="equalityComparer">An optional equality comparer; defaults to <see cref="EqualityComparer{T}.Default"/>.</param>
/// <typeparam name="TSubject">The type of the subject.</typeparam>
internal readonly struct BeOneOf<TSubject>(
    IReadOnlyCollection<TSubject> expectedValues,
    IEqualityComparer<TSubject>? equalityComparer = null) : IAssertionItem<TSubject>
{
    /// <inheritdoc/>
    public bool IsPassed(TSubject subject)
    {
        return expectedValues.Contains(subject, equalityComparer ?? EqualityComparer<TSubject>.Default);
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeOneOf(
            AssertionHelpers.GetObjectsString(expectedValues)));
    }
}