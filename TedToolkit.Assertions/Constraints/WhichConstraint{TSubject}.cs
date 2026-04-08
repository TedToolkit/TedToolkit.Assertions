// -----------------------------------------------------------------------
// <copyright file="WhichConstraint{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Constraints;

/// <summary>
/// Allows starting a new assertion chain on an item extracted from a previous assertion (accessed via <c>.Which</c>).
/// </summary>
/// <typeparam name="TSubject">The type of the extracted item.</typeparam>
public readonly record struct WhichConstraint<TSubject>
{
    private readonly SubjectInfo<TSubject> _info;

    private readonly bool _immediately;

    /// <summary>
    /// Initializes a new instance of the <see cref="WhichConstraint{TSubject}"/> struct.
    /// </summary>
    /// <param name="info">The subject info for the extracted item.</param>
    /// <param name="immediately">Whether subsequent assertions should evaluate immediately.</param>
    internal WhichConstraint(scoped in SubjectInfo<TSubject> info, bool immediately)
    {
        _info = info;
        _immediately = immediately;
    }

    /// <summary>
    /// Gets a <see cref="AssertionType.MUST"/>-level assertion on the extracted item.
    /// </summary>
    public ObjectAssertion<TSubject> Must
    {
        get
        {
            return new(AssertionType.MUST, _info, false, _immediately);
        }
    }

    /// <summary>
    /// Gets a <see cref="AssertionType.SHOULD"/>-level assertion on the extracted item.
    /// </summary>
    public ObjectAssertion<TSubject> Should
    {
        get
        {
            return new(AssertionType.SHOULD, _info, false, _immediately);
        }
    }

    /// <summary>
    /// Gets a <see cref="AssertionType.COULD"/>-level assertion on the extracted item.
    /// </summary>
    public ObjectAssertion<TSubject> Could
    {
        get
        {
            return new(AssertionType.COULD, _info, false, _immediately);
        }
    }
}