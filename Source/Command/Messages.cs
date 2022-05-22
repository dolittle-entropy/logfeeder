// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;

namespace Command;

/// <summary>
/// Defines the log messages to feed out.
/// </summary>
internal static partial class Messages
{
    [LoggerMessage(1, LogLevel.Information, "The user {username} has done something")]
    internal static partial void Information(this ILogger logger, string username);
}