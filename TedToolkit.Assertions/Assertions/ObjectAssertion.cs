// -----------------------------------------------------------------------
// <copyright file="ObjectAssertion.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Represents a single assertion on a subject, carrying the severity level, subject metadata, and evaluation modifiers.
/// </summary>
/// <param name="Type">The assertion severity level.</param>
/// <param name="Info">The subject value and its associated metadata.</param>
/// <param name="IsInverted">Whether the assertion logic is negated (via <c>.Not</c>).</param>
/// <param name="IsImmediately">Whether the assertion should evaluate immediately, bypassing scope collection.</param>
/// <typeparam name="TSubject">The type of the subject being asserted.</typeparam>
public readonly record struct ObjectAssertion<TSubject>(
    AssertionType Type,
    scoped in SubjectInfo<TSubject> Info,
    bool IsInverted,
    bool IsImmediately);