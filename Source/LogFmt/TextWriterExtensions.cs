// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace LogFmt;

/// <summary>
/// Extension methods for <see cref="TextWriter"/> to simplify writing LogFmt text.
/// </summary>
public static class TextWriterExtensions
{
    /// <summary>
    /// Writes a LogFmt ident to the writer.
    /// </summary>
    /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
    /// <param name="ident">The ident to write.</param>
    public static void WriteIdent(this TextWriter writer, string ident)
    {
        if (ident.Contains('"') || ident.Contains('='))
            throw new ArgumentException("LogFmt ident cannot contain \" or =", nameof(ident));

        writer.Write(ident.Replace(' ', '%'));
    }

    /// <summary>
    /// Writes a LogFmt value to the writer using the specified format and arguments.
    /// Uses the same semantics as <see cref="string.Format(System.IFormatProvider?,string,object?)"/>.
    /// </summary>
    /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
    /// <param name="format">The format to use for writing the value.</param>
    /// <param name="args">The optional arguments to use in the formatting.</param>
    public static void WriteValue(this TextWriter writer, string format, params object?[] args)
    {
        var value = string.Format(writer.FormatProvider, format, args);

        if (!value.Contains(' ') && !value.Contains('=') && !value.Contains('"'))
        {
            writer.Write(value);
            return;
        }

        var escaped = value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        writer.Write('"');
        writer.Write(escaped);
        writer.Write('"');
    }

    /// <summary>
    /// Writes a LogFmt pair (ident=value) to the writer using the specified ident, format and arguments.
    /// Uses the same semantics as <see cref="string.Format(System.IFormatProvider?,string,object?)"/>.
    /// </summary>
    /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
    /// <param name="ident">The ident to write.</param>
    /// <param name="format">The format to use for writing the value.</param>
    /// <param name="args">The optional arguments to use in the formatting.</param>
    public static void WritePair(this TextWriter writer, string ident, string format, params object?[] args)
    {
        writer.WriteIdent(ident);
        writer.Write('=');
        writer.WriteValue(format, args);
        writer.Write(' ');
    }
}