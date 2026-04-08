// -----------------------------------------------------------------------
// <copyright file="ContainSingleComparer.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;
#pragma warning disable CA1815

/// <summary>
/// Asserts that the collection contains exactly one item equal to the expected value, using an <see cref="IComparer{T}"/>. The matched item is extractable via <c>.Which</c>.
/// </summary>
/// <typeparam name="TSubject">The collection type.</typeparam>
/// <typeparam name="TItem">The element type.</typeparam>
/// <param name="expectedValue">The value to match.</param>
/// <param name="comparer">An optional comparer; defaults to <see cref="Comparer{T}.Default"/>.</param>
[AssertionMethodName("ContainSingle")]
internal struct ContainSingleComparer<TSubject, TItem>(
    TItem expectedValue,
    IComparer<TItem>? comparer = null) : IAssertionItem<TSubject, TItem>
    where TSubject : IReadOnlyCollection<TItem>
{
    private int _count;

    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        var realComparer = comparer ?? Comparer<TItem>.Default;

        var items = new List<TItem>();
        foreach (var item in subject)
        {
            if (realComparer.Compare(item, expectedValue) is 0)
            {
                items.Add(item);
            }
        }

        if (items.Count is not 1)
        {
            _count = items.Count;
            return false;
        }

        Item = items[0];
        return true;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(
            Localization.ExpectedStatements.ContainSingleItem(AssertionHelpers.GetObjectString(expectedValue)),
            Localization.ActualStatements.ContainSingle(_count.ToString(CultureInfo.InvariantCulture)));
    }

    /// <inheritdoc/>
    public WhichAssertionResult<TItem> Item { get; private set; }

    /// <inheritdoc/>
    public string OperatorName
    {
        get
        {
            return AssertionHelpers.OperationItem("SingleBy", expectedValue);
        }
    }
}