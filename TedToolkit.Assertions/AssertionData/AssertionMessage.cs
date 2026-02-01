// -----------------------------------------------------------------------
// <copyright file="AssertionMessage.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.AssertionData;

/// <summary>
/// An assertion Message.
/// </summary>
/// <param name="Type">Assertion Type.</param>
/// <param name="Message">message.</param>
/// <param name="Reason">reason.</param>
/// <param name="Tag">tag.</param>
public readonly record struct AssertionMessage(AssertionType Type, string Message, string Reason, object? Tag);