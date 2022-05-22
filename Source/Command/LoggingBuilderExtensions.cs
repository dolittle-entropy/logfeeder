// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using LogFmt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Command;

/// <summary>
/// Extension methods for <see cref="ILoggingBuilder"/> to configure log output using environmental variables.
/// </summary>
public static class LoggingBuilderExtensions
{
    /// <summary>
    /// Configures the output formatter for the loggers using the "formatter" and "colors" environmental variables. 
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for continuation.</returns>
    public static ILoggingBuilder ConfigureFormatter(this ILoggingBuilder builder)
    {
        switch (Environment.GetEnvironmentVariable("formatter")?.ToLowerInvariant())
        {
            case "logfmt":
                builder.AddLogFmtConsole(_ =>
                {
                    _.UseUtcTimestamp = true;
                    _.TimestampFormat = "O";
                });
                break;
            case "json":
                builder.AddJsonConsole(_ =>
                {
                    _.UseUtcTimestamp = true;
                    _.TimestampFormat = "O";
                });
                break;
            default:
                builder.AddSimpleConsole(_ =>
                {
                    _.UseUtcTimestamp = true;
                    _.TimestampFormat = "O";

                    var useColors = Environment.GetEnvironmentVariable("colors")?.ToLowerInvariant() != "false";
                    _.ColorBehavior = useColors ? LoggerColorBehavior.Enabled : LoggerColorBehavior.Disabled;
                });
                break;
        }

        return builder;
    }

    /// <summary>
    /// Configures the minimum loglevel to output using the "level" environmental variable.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for continuation.</returns>
    public static ILoggingBuilder ConfigureLogLevel(this ILoggingBuilder builder)
    {
        switch (Environment.GetEnvironmentVariable("level")?.ToLowerInvariant())
        {
            case "crit":
            case "critical":
                builder.SetMinimumLevel(LogLevel.Critical);
                break;
            case "err":
            case "error":
                builder.SetMinimumLevel(LogLevel.Error);
                break;
            case "warn":
            case "warning":
                builder.SetMinimumLevel(LogLevel.Warning);
                break;
            case "info":
            case "information":
                builder.SetMinimumLevel(LogLevel.Information);
                break;
            case "debug":
                builder.SetMinimumLevel(LogLevel.Debug);
                break;
            case "trace":
            default:
                builder.SetMinimumLevel(LogLevel.Trace);
                break;
        }

        return builder;
    }
}