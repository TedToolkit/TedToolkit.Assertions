// -----------------------------------------------------------------------
// <copyright file="AssertionType.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
///     The type of the assertion.
/// </summary>
public enum AssertionType
{
    /// <summary>
    ///     Advisory level — the weakest assertion; failures are informational.
    /// </summary>
    COULD = 0,

    /// <summary>
    ///     Recommended level — the subject is expected to satisfy the condition.
    /// </summary>
    SHOULD = 1,

    /// <summary>
    ///     Required level — the strongest assertion; failures indicate a hard violation.
    /// </summary>
    MUST = 2,
}