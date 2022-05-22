// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace LogFmt;

/// <summary>
/// Defines a <see cref="ConsoleFormatter"/> that formats log messages in the LogFmt format.
/// </summary>
public class Formatter : ConsoleFormatter
{
    readonly FormatterOptions _options;

    /// <summary>
    /// Initialises a new instance of the <see cref="Formatter"/> class.
    /// </summary>
    /// <param name="options">The options to use for the formatter.</param>
    public Formatter(IOptions<FormatterOptions> options)
        : base("logfmt")
    {
        _options = options.Value;
    }

    /// <inheritdoc />
    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
    {
        var message = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception);
        if (message == null && logEntry.Exception == null)
            return;

        if (_options.TimestampFormat != null)
            WriteTimestamp(textWriter, _options.UseUtcTimestamp, _options.TimestampFormat);

        if (logEntry.LogLevel != LogLevel.None)
            WriteLevel(textWriter, logEntry.LogLevel);

        if (logEntry.EventId.Id != 0)
            WriteEventId(textWriter, logEntry.EventId);

        if (logEntry.Category != null)
            WriteModule(textWriter, logEntry.Category);

        if (message != null)
            WriteMessage(textWriter, message);
        else
            WriteException(textWriter, logEntry.Exception!);

        if (logEntry.State is IReadOnlyCollection<KeyValuePair<string, object>> state)
            WriteState(textWriter, state);

        if (_options.IncludeScopes && scopeProvider != null)
            WriteScope(textWriter, logEntry.State, scopeProvider);
    }

    static void WriteTimestamp(TextWriter writer, bool useUtc, string format)
    {
        writer.WritePair("ts", (useUtc ? DateTime.UtcNow : DateTime.Now).ToString(format));
    }

    static void WriteLevel(TextWriter writer, LogLevel level)
    {
        writer.WritePair("level", level switch
        {
            LogLevel.Trace => "trace",
            LogLevel.Debug => "debug",
            LogLevel.Information => "info",
            LogLevel.Warning => "warn",
            LogLevel.Error => "error",
            LogLevel.Critical => "crit",
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, "Unknown log level")
        });
    }

    static void WriteEventId(TextWriter writer, EventId eventId)
    {
        writer.WritePair("event_id", eventId.Id.ToString());
    }

    static void WriteModule(TextWriter writer, string module)
    {
        writer.WritePair("module", module);
    }

    static void WriteMessage(TextWriter writer, string message)
    {
        writer.WritePair("msg", message);
    }

    static void WriteException(TextWriter writer, Exception exception)
    {
        writer.WritePair("exception", exception.GetType().Name);
        writer.WritePair("err", exception.Message);
    }

    static void WriteState(TextWriter writer, IReadOnlyCollection<KeyValuePair<string, object>> state)
    {
        foreach (var (key, value) in state)
        {
            if (key == "{OriginalFormat}")
                continue;

            writer.WritePair(key, "{0}", value);
        }
    }

    static void WriteScope<TState>(TextWriter writer, TState state, IExternalScopeProvider scopeProvider)
    {
        scopeProvider.ForEachScope((scope, _) =>
        {
            if (scope is not IReadOnlyCollection<KeyValuePair<string, object>> properties)
                return;

            foreach (var (key, value) in properties) writer.WritePair(key, "{0}", value);
        }, state);
    }
}