// -----------------------------------------------------------------------
// <copyright file="MatchRegexOne.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;

/// <summary>
/// Asserts that the string subject matches the regular expression. The first match is extractable via <c>.Which</c>.
/// </summary>
/// <param name="regularExpression">The regular expression pattern.</param>
[AssertionMethodName("MatchRegex")]
internal struct MatchRegexOne([StringSyntax(StringSyntaxAttribute.Regex)] string regularExpression)
    : IAssertionItem<string, string>
{
    /// <inheritdoc/>
    public bool IsPassed(string subject)
    {
        var result = new Regex(regularExpression).Match(subject);
        if (result.Success)
        {
            Item = result.Value;
        }

        return result.Success;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<string> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.Match(regularExpression));
    }

    /// <inheritdoc/>
    public WhichAssertionResult<string> Item { get; private set; }

    /// <inheritdoc/>
    public string OperatorName
    {
        get
        {
            return AssertionHelpers.OperationCode("Regex", regularExpression);
        }
    }
}