// -----------------------------------------------------------------------
// <copyright file="AssertionMethodNameAttribute.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;

namespace TedToolkit.Assertions.Attributes;

/// <summary>
/// The assertion method name attribute.
/// </summary>
/// <param name="name">name.</param>
[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
#pragma warning disable CS9113, CA1019
public sealed class AssertionMethodNameAttribute(string name) : Attribute;
#pragma warning restore CS9113, CA1019