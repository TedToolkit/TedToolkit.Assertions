// -----------------------------------------------------------------------
// <copyright file="ObjectAssertionExtensions.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Collections;

namespace TedToolkit.Assertions;

/// <summary>
/// Provides modifier properties (<c>.Immediately</c> and <c>.Not</c>) and helper methods on <see cref="ObjectAssertion{TSubject}"/>.
/// </summary>
public static class ObjectAssertionExtensions
{
#pragma warning disable CA1034
    extension<TSubject>(ObjectAssertion<TSubject> objectAssertion)
#pragma warning restore CA1034
    {
        /// <summary>
        /// Gets the assertion modified to evaluate immediately, bypassing scope collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">This modifier has already been applied.</exception>
        public ObjectAssertion<TSubject> Immediately
        {
            get
            {
                if (objectAssertion.IsImmediately)
                {
                    throw new InvalidOperationException(
                        "You shouldn't call Immediately at the case that you already did.");
                }

                return objectAssertion with { IsImmediately = true };
            }
        }

        /// <summary>
        /// Gets the assertion negated so that it passes when the original condition would fail, and vice versa.
        /// </summary>
        /// <exception cref="InvalidOperationException">This modifier has already been applied.</exception>
        public ObjectAssertion<TSubject> Not
        {
            get
            {
                if (objectAssertion.IsInverted)
                {
                    throw new InvalidOperationException(
                        "You shouldn't call Not at the case that you already did.");
                }

                return objectAssertion with { IsInverted = true };
            }
        }
    }

    /// <summary>
    /// Builds a localized assertion failure message from the expected and actual statements.
    /// </summary>
    /// <param name="assertion">The assertion context.</param>
    /// <param name="expectedStatement">A phrase describing the expected condition (e.g. "be null").</param>
    /// <param name="actualStatement">A phrase describing the actual value; defaults to the subject's string representation.</param>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <returns>The formatted failure message.</returns>
    public static string GetAssertionItemMessage<TSubject>(this scoped in ObjectAssertion<TSubject> assertion,
        string expectedStatement, string actualStatement = "")
    {
        if (string.IsNullOrEmpty(actualStatement))
        {
            actualStatement =
                Localization.ActualStatements.ItIs(AssertionHelpers.GetObjectString(assertion.Info.Subject));
        }

        if (assertion.IsInverted)
        {
            return Localization.AssertionGeneral.Assertion.Reversed(assertion.Info.Info.SubjectName,
                AssertionHelpers.Translate(assertion.Type),
                expectedStatement,
                actualStatement);
        }

        return Localization.AssertionGeneral.Assertion.Normal(assertion.Info.Info.SubjectName,
            AssertionHelpers.Translate(assertion.Type),
            expectedStatement,
            actualStatement);
    }
}