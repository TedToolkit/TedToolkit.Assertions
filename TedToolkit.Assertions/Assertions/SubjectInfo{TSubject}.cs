// -----------------------------------------------------------------------
// <copyright file="SubjectInfo{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Holds the subject value together with its associated metadata.
/// </summary>
/// <param name="Subject">The subject value being asserted.</param>
/// <param name="Info">The metadata describing the subject (name, caller info, and creation time).</param>
/// <typeparam name="TSubject">The type of the subject.</typeparam>
public readonly record struct SubjectInfo<TSubject>(TSubject Subject, SubjectInfo Info);