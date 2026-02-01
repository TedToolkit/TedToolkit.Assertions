// -----------------------------------------------------------------------
// <copyright file="WhichConstraint{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Constraints;

/// <summary>
/// The which constraint.
/// </summary>
/// <typeparam name="TSubject">the subject item.</typeparam>
public readonly record struct WhichConstraint<TSubject>
{
    private readonly SubjectInfo<TSubject> _info;

    private readonly bool _immediately;

    /// <summary>
    /// Initializes a new instance of the <see cref="WhichConstraint{TSubject}"/> struct.
    /// Constructor.
    /// </summary>
    /// <param name="info">the info.</param>
    /// <param name="immediately">immediately.</param>
    internal WhichConstraint(scoped in SubjectInfo<TSubject> info, bool immediately)
    {
        _info = info;
        _immediately = immediately;
    }

    /// <summary>
    /// Gets the must assertion.
    /// </summary>
    public ObjectAssertion<TSubject> Must
        => new(AssertionType.MUST, _info, false, _immediately);

    /// <summary>
    /// Gets the should assertion.
    /// </summary>
    public ObjectAssertion<TSubject> Should
        => new(AssertionType.SHOULD, _info, false, _immediately);

    /// <summary>
    /// Gets the could assertion.
    /// </summary>
    public ObjectAssertion<TSubject> Could
        => new(AssertionType.COULD, _info, false, _immediately);
}