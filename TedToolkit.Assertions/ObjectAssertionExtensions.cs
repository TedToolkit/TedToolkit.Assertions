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
/// The extensions for the assertions.
/// </summary>
public static class ObjectAssertionExtensions
{
#pragma warning disable CA1034
    extension<TSubject>(ObjectAssertion<TSubject> objectAssertion)
#pragma warning restore CA1034
    {
        /// <summary>
        /// Gets immediately.
        /// </summary>
        /// <exception cref="InvalidOperationException">it is already immediately.</exception>
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
        /// Gets revert.
        /// </summary>
        /// <exception cref="InvalidOperationException">it is already reverted.</exception>
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
    /// Get the assertion message.
    /// </summary>
    /// <param name="assertion">assertion.</param>
    /// <param name="expectedStatement">statement.</param>
    /// <param name="actualStatement">subject name.</param>
    /// <typeparam name="TSubject">the type of the subject.</typeparam>
    /// <returns>result.</returns>
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