// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Command;
using Microsoft.Extensions.Logging;

var factory = LoggerFactory.Create(_ => _
    .ConfigureFormatter()
    .ConfigureLogLevel()
);

var logger = factory.CreateLogger<Program>();

logger.Information("jakob");


Console.WriteLine("Wæt");