// -----------------------------------------------------------------------
// <copyright file="AssertionMessage.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.AssertionData;

/// <summary>
/// Encapsulates the result of a failed assertion: its severity, formatted message, optional reason, and optional tag.
/// </summary>
/// <param name="Type">The assertion severity level.</param>
/// <param name="Message">The formatted failure message.</param>
/// <param name="Reason">An optional user-supplied reason describing why this assertion exists.</param>
/// <param name="Tag">An optional user-supplied object for categorizing or filtering assertion results.</param>
public readonly record struct AssertionMessage(AssertionType Type, string Message, string Reason, object? Tag);