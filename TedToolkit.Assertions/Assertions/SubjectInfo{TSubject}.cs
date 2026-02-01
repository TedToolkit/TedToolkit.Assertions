// -----------------------------------------------------------------------
// <copyright file="SubjectInfo{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// The subject info.
/// </summary>
/// <param name="Subject">subject.</param>
/// <param name="Info">the info of the subject.</param>
/// <typeparam name="TSubject">type of the subject.</typeparam>
public readonly record struct SubjectInfo<TSubject>(TSubject Subject, SubjectInfo Info);