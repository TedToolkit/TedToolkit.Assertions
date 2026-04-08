// -----------------------------------------------------------------------
// <copyright file="IAssertionItem{TSubject,TItem}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// An assertion check that also extracts an item from the subject, enabling <c>.Which</c> chaining.
/// Implementors should be <b>internal struct</b>s so the source generator can discover them.
/// </summary>
/// <typeparam name="TSubject">The type of the subject being asserted.</typeparam>
/// <typeparam name="TItem">The type of the item extracted from the subject.</typeparam>
public interface IAssertionItem<TSubject, TItem> :
    IAssertionItem<TSubject>
{
    /// <summary>
    /// Gets the item extracted during <see cref="IAssertionItem{TSubject}.IsPassed"/> evaluation.
    /// </summary>
    WhichAssertionResult<TItem> Item { get; }

    /// <summary>
    /// Gets the operator name used to build the sub-operation label for the extracted item.
    /// </summary>
    string OperatorName { get; }
}