// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;

namespace Command;

/// <summary>
/// Defines the log messages to feed out.
/// </summary>
internal static partial class Messages
{
    [LoggerMessage(1, LogLevel.Information, "Using formatter {formatter} with colors {usecolors} and time {printtime} at loglevel {loglevel}. Configure these with the 'FORMATTER', 'COLORS', 'LEVEL' environmental variables.")]
    internal static partial void Usage(this ILogger logger, Formatter formatter, bool useColors, bool printTime, LogLevel logLevel);

    [LoggerMessage(2, LogLevel.Warning, "This program does not really have a purpose, I will just sit here and print log messages forever. If you were expecting something else, you should probably run another program. But I can't really help you figure out which one. If you find out, please let me know 👍")]
    internal static partial void ProgramHasNoPurpose(this ILogger logger);

    [LoggerMessage(3, LogLevel.Error, "An exception was thrown.")]
    internal static partial void SomethingWentWrong(this ILogger logger, Exception exception);

    [LoggerMessage(4, LogLevel.Critical, "A critical error occurred, but I will pretend like I can continue.")]
    internal static partial void SomethingWentReallyWrong(this ILogger logger, Exception exception);

    [LoggerMessage(5, LogLevel.Debug, "The service is starting some important work, the count is {count}")]
    internal static partial void StartingImportantWork(this ILogger logger, long count);

    [LoggerMessage(6, LogLevel.Trace, "The service is currently working in {cwd} on {command} with {pid}")]
    internal static partial void CurrentEnvironment(this ILogger logger, string cwd, string command, int pid);
}