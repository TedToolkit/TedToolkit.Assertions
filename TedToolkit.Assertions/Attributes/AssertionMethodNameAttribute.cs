// -----------------------------------------------------------------------
// <copyright file="AssertionMethodNameAttribute.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;

namespace TedToolkit.Assertions.Attributes;

/// <summary>
/// Specifies the name of the extension method that the source generator will create for this assertion item.
/// Apply multiple times to generate aliases (e.g. <c>Be</c> and <c>BeEqualTo</c>).
/// </summary>
/// <param name="name">The desired extension method name.</param>
[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
#pragma warning disable CS9113, CA1019
public sealed class AssertionMethodNameAttribute(string name) : Attribute;
#pragma warning restore CS9113, CA1019