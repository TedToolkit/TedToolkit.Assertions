// -----------------------------------------------------------------------
// <copyright file="AssertionParameterNameAttribute.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;

namespace TedToolkit.Assertions.Attributes;

/// <summary>
/// The assertion method parameter name attribute.
/// </summary>
/// <param name="parameterName">name of the parameter.</param>
[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.Parameter)]
#pragma warning disable CS9113, CA1019
public sealed class AssertionParameterNameAttribute(string parameterName) : Attribute;
#pragma warning restore CS9113, CA1019