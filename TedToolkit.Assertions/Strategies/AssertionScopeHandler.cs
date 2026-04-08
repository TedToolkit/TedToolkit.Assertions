// -----------------------------------------------------------------------
// <copyright file="AssertionScopeHandler.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Strategies;

/// <summary>
/// Delegate invoked when an <see cref="AssertionScope"/> exits, receiving all collected assertion failures.
/// </summary>
/// <param name="scope">The scope containing the collected assertion messages.</param>
public delegate void AssertionScopeHandler(scoped in AssertionScope scope);