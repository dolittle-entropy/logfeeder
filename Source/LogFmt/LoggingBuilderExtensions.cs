// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LogFmt;

/// <summary>
/// Extension methods for <see cref="ILoggingBuilder"/> for the LogFmt formatter.
/// </summary>
public static class LoggingBuilderExtensions
{
    /// <summary>
    /// Add and configure a console log formatter named 'logfmt' to the factory.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <param name="configure">A delegate to configure the options for the logfmt log formatter.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for continuation.</returns>
    public static ILoggingBuilder AddLogFmtConsole(this ILoggingBuilder builder, Action<FormatterOptions> configure)
    {
        builder.AddConsoleFormatter<Formatter, FormatterOptions>();
        builder.AddConsole(_ => _.FormatterName = "logfmt");
        builder.Services.Configure(configure);
        return builder;
    }
}