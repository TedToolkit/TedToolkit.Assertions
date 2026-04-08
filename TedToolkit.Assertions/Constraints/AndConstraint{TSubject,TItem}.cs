// -----------------------------------------------------------------------
// <copyright file="AndConstraint{TSubject,TItem}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Cysharp.Text;

namespace TedToolkit.Assertions.Constraints;

/// <summary>
/// Returned by item-extracting assertion methods to enable fluent chaining and <see cref="Which"/> access to the extracted item.
/// </summary>
/// <typeparam name="TSubject">The type of the subject being asserted.</typeparam>
/// <typeparam name="TItem">The type of the item extracted by the assertion.</typeparam>
public readonly record struct AndConstraint<TSubject, TItem>
{
    private readonly string _itemName;

    private readonly WhichAssertionResult<TItem> _item;

    /// <summary>
    /// Initializes a new instance of the <see cref="AndConstraint{TSubject, TItem}"/> struct.
    /// </summary>
    /// <param name="assertion">The assertion to continue chaining from.</param>
    /// <param name="item">The extracted item result.</param>
    /// <param name="itemName">The operator name used to build the sub-operation label.</param>
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
    /// Gets the assertion for chaining additional checks on the same subject.
    /// </summary>
    public ObjectAssertion<TSubject> And { get; }

    /// <summary>
    /// Gets a pronoun constraint that allows switching the assertion level (e.g. <c>.AndIt.Should</c>).
    /// </summary>
    public PronounConstraint<TSubject> AndIt
    {
        get
        {
            return new(And);
        }
    }

    /// <summary>
    /// Gets the subject value.
    /// </summary>
    public TSubject Subject
    {
        get
        {
            return And.Info.Subject;
        }
    }

    /// <summary>
    /// Gets the extracted item value.
    /// </summary>
    public TItem SubjectItem
    {
        get
        {
            return _item.Value;
        }
    }

    /// <summary>
    /// Gets a <see cref="WhichConstraint{TSubject}"/> that allows starting a new assertion chain on the extracted item.
    /// </summary>
    public WhichConstraint<TItem> Which
    {
        get
        {
            return new(new SubjectInfo<TItem>(SubjectItem, And.Info.Info.SubOperation(_itemName)), And.IsImmediately);
        }
    }
}