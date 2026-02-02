// -----------------------------------------------------------------------
// <copyright file="AllSatisfy.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;

/// <summary>
/// All satisfy.
/// </summary>
/// <param name="predicate">predicate.</param>
/// <param name="predicateName">the predicate name.</param>
/// <typeparam name="TSubject">subject type.</typeparam>
/// <typeparam name="TItem">item type.</typeparam>
internal readonly struct AllSatisfy<TSubject, TItem>(
    Func<TItem, bool> predicate,
    [AssertionParameterName(nameof(predicate))]
    string predicateName = "")
    : IAssertionItem<TSubject>
    where TSubject : IReadOnlyCollection<TItem>
{
    private readonly List<int> _failedIndexes = [];

    /// <inheritdoc/>
    public bool IsPassed(TSubject subject)
    {
        _failedIndexes.Clear();
        var index = 0;
        foreach (var item in subject)
        {
            try
            {
                if (!predicate(item))
                    _failedIndexes.Add(index);
            }
            finally
            {
                index++;
            }
        }

        return _failedIndexes.Count is 0;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(
            Localization.ExpectedStatements.AllSatisfy(predicateName),
            Localization.ActualStatements.AllSatisfy(AssertionHelpers.GetObjectsString(_failedIndexes)));
    }
}