// -----------------------------------------------------------------------
// <copyright file="AndConstraint{TSubject,TItem}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Cysharp.Text;

namespace TedToolkit.Assertions.Constraints;

/// <summary>
/// The 'and' constraint.
/// </summary>
/// <typeparam name="TSubject">the type of the object.</typeparam>
/// <typeparam name="TItem">the item.</typeparam>
public readonly record struct AndConstraint<TSubject, TItem>
{
    private readonly string _itemName;

    private readonly WhichAssertionResult<TItem> _item;

    /// <summary>
    /// Initializes a new instance of the <see cref="AndConstraint{TSubject, TItem}"/> struct.
    /// Constructor.
    /// </summary>
    /// <param name="assertion">the assertion.</param>
    /// <param name="item">the item.</param>
    /// <param name="itemName">the item name.</param>
    internal AndConstraint(
        scoped in ObjectAssertion<TSubject> assertion,
        scoped in WhichAssertionResult<TItem> item,
        string itemName)
    {
        _item = item;
        _itemName = itemName;
        And = assertion;
    }

    /// <summary>
    /// Gets 'And' assertion.
    /// </summary>
    public ObjectAssertion<TSubject> And { get; }

    /// <summary>
    /// Gets 'And It' assertion.
    /// </summary>
    public PronounConstraint<TSubject> AndIt
        => new(And);

    /// <summary>
    /// Gets get the Subject.
    /// </summary>
    public TSubject Subject
        => And.Info.Subject;

    /// <summary>
    /// Gets the item value.
    /// </summary>
    public TItem SubjectItem
        => _item.Value;

    /// <summary>
    /// Gets get the item thing.
    /// </summary>
    public WhichConstraint<TItem> Which
        => new(new SubjectInfo<TItem>(SubjectItem, And.Info.Info.SubOperation(_itemName)), And.IsImmediately);
}