// -----------------------------------------------------------------------
// <copyright file="ContainSinglePredicate.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;
#pragma warning disable CA1815

/// <summary>
/// Asserts that the collection contains exactly one item matching the predicate. The matched item is extractable via <c>.Which</c>.
/// </summary>
/// <param name="predicate">The predicate to match against.</param>
/// <param name="predicateName">The captured expression of the predicate (auto-filled by the source generator).</param>
/// <typeparam name="TSubject">The collection type.</typeparam>
/// <typeparam name="TItem">The element type.</typeparam>
[AssertionMethodName("ContainSingle")]
internal struct ContainSinglePredicate<TSubject, TItem>(
    Func<TItem, bool> predicate,
    [AssertionParameterName(nameof(predicate))]
    string predicateName = "") : IAssertionItem<TSubject, TItem>
    where TSubject : IReadOnlyCollection<TItem>
{
    private int _count;

    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var items = subject.Where(predicate).ToArray();
        if (items.Length is not 1)
        {
            _count = items.Length;
            return false;
        }

        Item = items[0];
        return true;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(
            Localization.ExpectedStatements.ContainSinglePredicate(predicateName),
            Localization.ActualStatements.ContainSingle(_count.ToString(CultureInfo.InvariantCulture)));
    }

    /// <inheritdoc/>
    public WhichAssertionResult<TItem> Item { get; private set; }

    /// <inheritdoc/>
    public string OperatorName
    {
        get
        {
            return AssertionHelpers.OperationCode("SingleBy", predicateName);
        }
    }
}