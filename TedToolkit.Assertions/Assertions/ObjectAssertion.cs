// -----------------------------------------------------------------------
// <copyright file="ObjectAssertion.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Some things that related to the assertions, every assertion may be changed.
/// </summary>
/// <param name="Type">Assertion type.</param>
/// <param name="Info">info.</param>
/// <param name="IsInverted">is inverted.</param>
/// <param name="IsImmediately">is immediately.</param>
/// <typeparam name="TSubject">the type of the assertion.</typeparam>
public readonly record struct ObjectAssertion<TSubject>(
    AssertionType Type,
    scoped in SubjectInfo<TSubject> Info,
    bool IsInverted,
    bool IsImmediately);