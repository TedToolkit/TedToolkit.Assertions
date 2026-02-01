// -----------------------------------------------------------------------
// <copyright file="AssertionExtensions.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace TedToolkit.Assertions;

/// <summary>
/// The extension of.
/// </summary>
public static class AssertionExtensions
{
    /// <summary>
    ///     The must assertion.
    /// </summary>
    /// <inheritdoc cref="CreateInfo"/>
    /// <returns>assertion object.</returns>
    public static ObjectAssertion<TSubject> Must<TSubject>(this TSubject subject,
        [CallerArgumentExpression(nameof(subject))]
        string subjectName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        return new(AssertionType.MUST,
            CreateInfo(subject, subjectName, memberName, filePath, lineNumber),
            false, false);
    }

    /// <summary>
    ///     The should assertion.
    /// </summary>
    /// <inheritdoc cref="CreateInfo"/>
    /// <returns>assertion object.</returns>
    public static ObjectAssertion<TSubject> Should<TSubject>(this TSubject subject,
        [CallerArgumentExpression(nameof(subject))]
        string subjectName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        return new(AssertionType.SHOULD,
            CreateInfo(subject, subjectName, memberName, filePath, lineNumber),
            false, false);
    }

    /// <summary>
    ///     The could assertion.
    /// </summary>
    /// <inheritdoc cref="CreateInfo"/>
    /// <returns>assertion object.</returns>
    public static ObjectAssertion<TSubject> Could<TSubject>(this TSubject subject,
        [CallerArgumentExpression(nameof(subject))]
        string subjectName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        return new(AssertionType.COULD,
            CreateInfo(subject, subjectName, memberName, filePath, lineNumber),
            false, false);
    }

    /// <summary>
    /// Create the info of the data.
    /// </summary>
    /// <param name="subject">subject.</param>
    /// <param name="subjectName">subject name.</param>
    /// <param name="memberName">caller member name.</param>
    /// <param name="filePath">caller file path.</param>
    /// <param name="lineNumber">caller line number.</param>
    /// <typeparam name="TSubject">the type of the subject.</typeparam>
    /// <returns>info.</returns>
    private static SubjectInfo<TSubject> CreateInfo<TSubject>(TSubject subject,
        string subjectName,
        string memberName,
        string filePath,
        int lineNumber)
    {
        return new(subject,
            new SubjectInfo(subjectName, memberName, filePath, lineNumber, DateTimeOffset.Now));
    }
}