// -----------------------------------------------------------------------
// <copyright file="AssertionItemHandler.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.AssertionData;

namespace TedToolkit.Assertions.Strategies;

/// <summary>
/// The assertion item handler.
/// </summary>
/// <param name="info">subject info.</param>
/// <param name="message">assert message.</param>
public delegate void AssertionItemHandler(scoped in SubjectInfo info, scoped in AssertionMessage message);