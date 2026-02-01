// -----------------------------------------------------------------------
// <copyright file="AssertionScopeHandler.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Strategies;

/// <summary>
/// The assertion scope handler.
/// </summary>
/// <param name="scope">the scope.</param>
public delegate void AssertionScopeHandler(scoped in AssertionScope scope);