// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;

namespace Command;

/// <summary>
/// Defines methods to get the LogFeeder configuration from environment variables.
/// </summary>
public static class Configuration 
{
    /// <summary>
    /// Gets the log formatter to use for writing log messages to the standard output.
    /// </summary>
    /// <returns>A <see cref="Formatter"/> that indicates which formatter to use.</returns>
    public static Formatter GetFormatter()
        => Environment.GetEnvironmentVariable("FORMATTER")?.ToLowerInvariant() switch
        {
            "logfmt" => Formatter.LogFmt,
            "json" => Formatter.Json,
            _ => Formatter.Simple
        };
    
    /// <summary>
    /// Checks whether or not the time should be printed in the log messages.
    /// </summary>
    /// <returns>True if time should be printed, false if not.</returns>
    public static bool ShouldPrintTime()
        => Environment.GetEnvironmentVariable("TIME")?.ToLowerInvariant() != "false";
    
    /// <summary>
    /// Checks whether or not colors should be used when printing log messages.
    /// </summary>
    /// <returns>True if colors should be used, false if not.</returns>
    public static bool ShouldUseColors()
        => Environment.GetEnvironmentVariable("COLORS")?.ToLowerInvariant() != "false";

    /// <summary>
    /// Gets the minimum log level to print.
    /// </summary>
    /// <returns>The minimum <see cref="LogLevel"/> to print.</returns>
    public static LogLevel GetMinimumLogLevel()
        => Environment.GetEnvironmentVariable("LEVEL")?.ToLowerInvariant() switch
        {
            "crit" or "critical" => LogLevel.Critical,
            "err" or "error" => LogLevel.Error,
            "warn" or "warning" => LogLevel.Warning,
            "info" or "information" => LogLevel.Information,
            "debug" => LogLevel.Debug,
            "trace" or _ => LogLevel.Trace,
        };
}