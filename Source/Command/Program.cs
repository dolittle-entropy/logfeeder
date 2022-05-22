// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reactive.Linq;
using System.Security.Cryptography;
using Command;
using Microsoft.Extensions.Logging;

var factory = LoggerFactory.Create(_ => _
    .ConfigureFormatter()
    .ConfigureLogLevel()
);

var logger = factory.CreateLogger<Program>();
var serviceLogger = factory.CreateLogger<Service>();

Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10)).Subscribe(_ => logger.Usage(Configuration.GetFormatter(), Configuration.ShouldUseColors(), Configuration.ShouldPrintTime(), Configuration.GetMinimumLogLevel()));
Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(20)).Subscribe(_ => logger.ProgramHasNoPurpose());
Observable.Timer(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2)).Subscribe(_ => serviceLogger.StartingImportantWork(_));
Observable.Timer(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1)).Subscribe(_ => logger.CurrentEnvironment(Environment.CurrentDirectory, Environment.CommandLine, Environment.ProcessId));
Observable.Timer(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(20)).Subscribe(_ => logger.SomethingWentWrong(new ApplicationException("Some kind of exception that can be handled")));
Observable.Timer(TimeSpan.FromSeconds(8), TimeSpan.FromSeconds(20)).Subscribe(_ => logger.SomethingWentReallyWrong(new CryptographicException("Oh, this is really bad.")));

await Task.Delay(Timeout.InfiniteTimeSpan);

public class Service {}