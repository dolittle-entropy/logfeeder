# Dolittle LogFeeder 
A small application that generates different kinds of logs to the standard output for testing of log aggregation systems.
It will print a predefined set of log messages at different levels, with a formatting of your choice.

### How to run
The simplest way to run it is to use the published Docker image `dolittle/logfeeder`, for example:
````shell
$ docker run dolittle/logfeeder
2022-05-23T06:10:47.0723536+00:00 info: Program[1]
      Using formatter Simple with colors True and time True at loglevel Trace. Configure these with the 'FORMATTER', 'COLORS', 'LEVEL' environmental variables.
2022-05-23T06:10:48.0599255+00:00 warn: Program[2]
      This program does not really have a purpose, I will just sit here and print log messages forever. If you were expecting something else, you should probably run another program. But I can't really help you figure out which one. If you find out, please let me know 👍
2022-05-23T06:10:49.0625179+00:00 dbug: Service[5]
      The service is starting some important work, the count is 0
````

### Configuration
The application is configured with a set of environmental variables:

| Variable  | Values                     | Description                                                                  |
|-----------|----------------------------|------------------------------------------------------------------------------|
| FORMATTER | `simple`, `json`, `logfmt` | Selects the formatter to use for printing log messages. Defaults to `simple` |
| COLORS    | `true`, `false`            | Use VT characters to print colors when using the `simple` formatter.         |
| TIME      | `true`, `false`            | Print the current time as part of the log message.                           |

