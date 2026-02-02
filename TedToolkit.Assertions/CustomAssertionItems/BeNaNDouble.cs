// -----------------------------------------------------------------------
// <copyright file="BeNaNDouble.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;

/// <summary>
/// Be NaN.
/// </summary>
[AssertionMethodName("BeNaN")]
internal readonly struct BeNaNDouble : IAssertionItem<double>
{
    /// <inheritdoc/>
    public bool IsPassed(double subject)
        => double.IsNaN(subject);

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<double> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeNaN);
}