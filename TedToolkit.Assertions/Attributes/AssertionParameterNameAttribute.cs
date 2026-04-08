// -----------------------------------------------------------------------
// <copyright file="AssertionParameterNameAttribute.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;

namespace TedToolkit.Assertions.Attributes;

/// <summary>
/// Marks a constructor parameter so the source generator applies <c>[CallerArgumentExpression]</c> to the
/// corresponding generated method parameter, automatically capturing the caller's argument expression.
/// </summary>
/// <param name="parameterName">The name of the constructor parameter whose argument expression should be captured.</param>
[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.Parameter)]
#pragma warning disable CS9113, CA1019
public sealed class AssertionParameterNameAttribute(string parameterName) : Attribute;
#pragma warning restore CS9113, CA1019