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
    ///     Could way.
    /// </summary>
    COULD = 0,

    /// <summary>
    ///     Should way.
    /// </summary>
    SHOULD = 1,

    /// <summary>
    ///     Must way.
    /// </summary>
    MUST = 2,
}