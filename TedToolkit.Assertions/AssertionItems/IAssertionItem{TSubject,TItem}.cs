// -----------------------------------------------------------------------
// <copyright file="IAssertionItem{TSubject,TItem}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// The assertion item. make this type as <b>internal readonly struct</b>.
/// </summary>
/// <typeparam name="TSubject">the subject.</typeparam>
/// <typeparam name="TItem">the item type.</typeparam>
public interface IAssertionItem<TSubject, TItem> :
    IAssertionItem<TSubject>
{
    /// <summary>
    /// Gets the item that it calculated.
    /// </summary>
    WhichAssertionResult<TItem> Item { get; }

    /// <summary>
    /// Gets the operator name for operation.
    /// </summary>
    string OperatorName { get; }
}