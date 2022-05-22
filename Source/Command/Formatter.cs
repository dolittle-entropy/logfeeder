// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Command;

/// <summary>
/// Defines the available log formatters.
/// </summary>
public enum Formatter
{
    /// <summary>
    /// Writes log messages as human readable text.
    /// </summary>
    Simple,
    
    /// <summary>
    /// Writes log messages as JSON.
    /// </summary>
    Json,
    
    /// <summary>
    /// Writes log messages as LogFmt.
    /// </summary>
    LogFmt,
}