// -----------------------------------------------------------------------
// <copyright file="AssertionItemHandler.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.AssertionData;

namespace TedToolkit.Assertions.Strategies;

/// <summary>
/// Delegate invoked when an individual assertion fails (outside a scope, or when <c>.Immediately</c> is used inside a scope).
/// </summary>
/// <param name="info">The metadata of the subject that failed.</param>
/// <param name="message">The assertion failure details.</param>
public delegate void AssertionItemHandler(scoped in SubjectInfo info, scoped in AssertionMessage message);