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
    /// Configures the output formatter for the loggers. 
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for continuation.</returns>
    public static ILoggingBuilder ConfigureFormatter(this ILoggingBuilder builder)
    {
        switch (Configuration.GetFormatter())
        {
            case Formatter.LogFmt:
                builder.AddLogFmtConsole(_ =>
                {
                    _.UseUtcTimestamp = true;
                    if (Configuration.ShouldPrintTime())
                        _.TimestampFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";
                });
                break;
            case Formatter.Json:
                builder.AddJsonConsole(_ =>
                {
                    _.UseUtcTimestamp = true;
                    if (Configuration.ShouldPrintTime())
                        _.TimestampFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";
                });
                break;
            case Formatter.Simple:
                builder.AddSimpleConsole(_ =>
                {
                    _.UseUtcTimestamp = true;
                    if (Configuration.ShouldPrintTime())
                        _.TimestampFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK' '";
                    _.ColorBehavior =  Configuration.ShouldUseColors() ? LoggerColorBehavior.Enabled : LoggerColorBehavior.Disabled;
                });
                break;
        }
        
        return builder;
    }

    /// <summary>
    /// Configures the minimum loglevel to output.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for continuation.</returns>
    public static ILoggingBuilder ConfigureLogLevel(this ILoggingBuilder builder)
        => builder.SetMinimumLevel(Configuration.GetMinimumLogLevel());
}