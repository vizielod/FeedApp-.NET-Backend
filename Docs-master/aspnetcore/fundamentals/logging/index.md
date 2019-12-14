---
title: Logging in ASP.NET Core
author: ardalis
description: Learn about the logging framework in ASP.NET Core. Discover the built-in logging providers and learn more about popular third-party providers.
manager: wpickett
ms.author: tdykstra
ms.date: 12/15/2017
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: article
uid: fundamentals/logging/index
---
# Logging in ASP.NET Core

By [Steve Smith](https://ardalis.com/) and [Tom Dykstra](https://github.com/tdykstra)

ASP.NET Core supports a logging API that works with a variety of logging providers. Built-in providers let you send logs to one or more destinations, and you can plug in a third-party logging framework. This article shows how to use the built-in logging API and providers in your code.

# [ASP.NET Core 2.x](#tab/aspnetcore2x)

[View or download sample code](https://github.com/aspnet/Docs/tree/master/aspnetcore/fundamentals/logging/index/sample2) ([how to download](xref:tutorials/index#how-to-download-a-sample))

# [ASP.NET Core 1.x](#tab/aspnetcore1x)

[View or download sample code](https://github.com/aspnet/Docs/tree/master/aspnetcore/fundamentals/logging/index/sample) ([how to download](xref:tutorials/index#how-to-download-a-sample))

---

## How to create logs

To create logs, get an `ILogger` object from the [dependency injection](xref:fundamentals/dependency-injection) container:

[!code-csharp[](index/sample/Controllers/TodoController.cs?name=snippet_LoggerDI&highlight=7)]

Then call logging methods on that logger object:

[!code-csharp[](index/sample/Controllers/TodoController.cs?name=snippet_CallLogMethods&highlight=3,7)]

This example creates logs with the `TodoController` class as the *category*. Categories are explained [later in this article](#log-category).

ASP.NET Core doesn't provide async logger methods because logging should be so fast that it isn't worth the cost of using async. If you're in a situation where that's not true, consider changing the way you log. If your data store is slow, write the log messages to a fast store first, then move them to a slow store later. For example, log to a message queue that's read and persisted to slow storage by another process.

## How to add providers

# [ASP.NET Core 2.x](#tab/aspnetcore2x/)

A logging provider takes the messages that you create with an `ILogger` object and displays or stores them. For example, the Console provider displays messages on the console, and the Azure App Service provider can store them in Azure blob storage.

To use a provider, call the provider's `Add<ProviderName>` extension method in *Program.cs*:

[!code-csharp[](index/sample2/Program.cs?name=snippet_ExpandDefault&highlight=16,17)]

The default project template enables logging with the [CreateDefaultBuilder](/dotnet/api/microsoft.aspnetcore.webhost.createdefaultbuilder?view=aspnetcore-2.0#Microsoft_AspNetCore_WebHost_CreateDefaultBuilder_System_String___) method:

[!code-csharp[](index/sample2/Program.cs?name=snippet_TemplateCode&highlight=7)]

# [ASP.NET Core 1.x](#tab/aspnetcore1x/)

A logging provider takes the messages that you create with an `ILogger` object and displays or stores them. For example, the Console provider displays messages on the console, and the Azure App Service provider can store them in Azure blob storage.

To use a provider, install its NuGet package and call the provider's extension method on an instance of `ILoggerFactory`, as shown in the following example.

[!code-csharp[](index/sample//Startup.cs?name=snippet_AddConsoleAndDebug&highlight=3,5-7)]

ASP.NET Core [dependency injection](xref:fundamentals/dependency-injection) (DI) provides the `ILoggerFactory` instance. The `AddConsole` and `AddDebug` extension methods are defined in the [Microsoft.Extensions.Logging.Console](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Console/) and [Microsoft.Extensions.Logging.Debug](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Debug/) packages. Each extension method calls the `ILoggerFactory.AddProvider` method, passing in an instance of the provider. 

> [!NOTE]
> The sample application for this article adds logging providers in the `Configure` method of the `Startup` class. If you want to get log output from code that executes earlier, add logging providers in the `Startup` class constructor instead. 

---

You'll find information about each [built-in logging provider](#built-in-logging-providers) and links to [third-party logging providers](#third-party-logging-providers) later in the article.

## Sample logging output

With the sample code shown in the preceding section, you'll see logs in the console when you run from the command line. Here's an example of console output:

```console
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:5000/api/todo/0
info: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[1]
      Executing action method TodoApi.Controllers.TodoController.GetById (TodoApi) with arguments (0) - ModelState is Valid
info: TodoApi.Controllers.TodoController[1002]
      Getting item 0
warn: TodoApi.Controllers.TodoController[4000]
      GetById(0) NOT FOUND
info: Microsoft.AspNetCore.Mvc.StatusCodeResult[1]
      Executing HttpStatusCodeResult, setting HTTP status code 404
info: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[2]
      Executed action TodoApi.Controllers.TodoController.GetById (TodoApi) in 42.9286ms
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 148.889ms 404
```

These logs were created by going to `http://localhost:5000/api/todo/0`, which triggers execution of both `ILogger` calls shown in the preceding section.

Here's an example of the same logs as they appear in the Debug window when you run the sample application in Visual Studio:

```console
Microsoft.AspNetCore.Hosting.Internal.WebHost:Information: Request starting HTTP/1.1 GET http://localhost:53104/api/todo/0  
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker:Information: Executing action method TodoApi.Controllers.TodoController.GetById (TodoApi) with arguments (0) - ModelState is Valid
TodoApi.Controllers.TodoController:Information: Getting item 0
TodoApi.Controllers.TodoController:Warning: GetById(0) NOT FOUND
Microsoft.AspNetCore.Mvc.StatusCodeResult:Information: Executing HttpStatusCodeResult, setting HTTP status code 404
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker:Information: Executed action TodoApi.Controllers.TodoController.GetById (TodoApi) in 152.5657ms
Microsoft.AspNetCore.Hosting.Internal.WebHost:Information: Request finished in 316.3195ms 404 
```

The logs that were created by the `ILogger` calls shown in the preceding section begin with "TodoApi.Controllers.TodoController". The logs that begin with "Microsoft" categories are from ASP.NET Core. ASP.NET Core itself and your application code are using the same logging API and the same logging providers.

The remainder of this article explains some details and options for logging.

## NuGet packages

The `ILogger` and `ILoggerFactory` interfaces are in [Microsoft.Extensions.Logging.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions/), and default implementations for them are in [Microsoft.Extensions.Logging](https://www.nuget.org/packages/microsoft.extensions.logging/).

## Log category

A *category* is included with each log that you create. You specify the category when you create an `ILogger` object. The category may be any string, but a convention is to use the fully qualified name of the class from which the logs are written. For example: "TodoApi.Controllers.TodoController".

You can specify the category as a string or use an extension method that derives the category from the type. To specify the category as a string, call `CreateLogger` on an `ILoggerFactory` instance, as shown below.

[!code-csharp[](index/sample//Controllers/TodoController.cs?name=snippet_CreateLogger&highlight=7,10)]

Most of the time, it will be easier to use `ILogger<T>`, as in the following example.

[!code-csharp[](index/sample//Controllers/TodoController.cs?name=snippet_LoggerDI&highlight=7)]

This is equivalent to calling `CreateLogger` with the fully qualified type name of `T`.

## Log level

Each time you write a log, you specify its [LogLevel](/dotnet/api/microsoft.extensions.logging.logLevel). The log level indicates the degree of severity or importance. For example, you might write an `Information` log when a method ends normally, a `Warning` log when a method returns a 404 return code, and an `Error` log when you catch an unexpected exception.

In the following code example, the names of the methods (for example, `LogWarning`) specify the log level. The first parameter is the [Log event ID](#log-event-id). The second parameter is a [message template](#log-message-template) with placeholders for argument values provided by the remaining method parameters. The method parameters are explained in more detail later in this article.

[!code-csharp[](index/sample//Controllers/TodoController.cs?name=snippet_CallLogMethods&highlight=3,7)]

Log methods that include the level in the method name are [extension methods for ILogger](/dotnet/api/microsoft.extensions.logging.loggerextensions). Behind the scenes, these methods call a `Log` method that takes a `LogLevel` parameter. You can call the `Log` method directly rather than one of these extension methods, but the syntax is relatively complicated. For more information, see the [ILogger interface](/dotnet/api/microsoft.extensions.logging.ilogger) and the [logger extensions source code](https://github.com/aspnet/Logging/blob/master/src/Microsoft.Extensions.Logging.Abstractions/LoggerExtensions.cs).

ASP.NET Core defines the following [log levels](/dotnet/api/microsoft.extensions.logging.loglevel), ordered here from least to highest severity.

* Trace = 0

  For information that's valuable only to a developer debugging an issue. These messages may contain sensitive application data and so shouldn't be enabled in a production environment. *Disabled by default.* Example: `Credentials: {"User":"someuser", "Password":"P@ssword"}`

* Debug = 1

  For information that has short-term usefulness during development and debugging. Example: `Entering method Configure with flag set to true.` You typically wouldn't enable `Debug` level logs in production unless you are troubleshooting, due to the high volume of logs.

* Information = 2

  For tracking the general flow of the application. These logs typically have some long-term value. Example: `Request received for path /api/todo`

* Warning = 3

  For abnormal or unexpected events in the application flow. These may include errors or other conditions that don't cause the application to stop, but which may need to be investigated. Handled exceptions are a common place to use the `Warning` log level. Example: `FileNotFoundException for file quotes.txt.`

* Error = 4

  For errors and exceptions that cannot be handled. These messages indicate a failure in the current activity or operation (such as the current HTTP request), not an application-wide failure. Example log message: `Cannot insert record due to duplicate key violation.`

* Critical = 5

  For failures that require immediate attention. Examples: data loss scenarios, out of disk space.

You can use the log level to control how much log output is written to a particular storage medium or display window. For example, in production you might want all logs of `Information` level and lower to go to a volume data store, and all logs of `Warning` level and higher to go to a value data store. During development, you might normally send logs of `Warning` or higher severity to the console. Then when you need to troubleshoot, you can add `Debug` level. The [Log filtering](#log-filtering) section later in this article explains how to control which log levels a provider handles.

The ASP.NET Core framework writes `Debug` level logs for framework events. The log examples earlier in this article excluded logs below `Information` level, so no `Debug` level logs were shown. Here's an example of console logs if you run the sample application configured to show `Debug` and higher logs for the console provider.

```console
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:62555/api/todo/0
dbug: Microsoft.AspNetCore.Routing.Tree.TreeRouter[1]
      Request successfully matched the route with name 'GetTodo' and template 'api/Todo/{id}'.
dbug: Microsoft.AspNetCore.Mvc.Internal.ActionSelector[2]
      Action 'TodoApi.Controllers.TodoController.Update (TodoApi)' with id '089d59b6-92ec-472d-b552-cc613dfd625d' did not match the constraint 'Microsoft.AspNetCore.Mvc.Internal.HttpMethodActionConstraint'
dbug: Microsoft.AspNetCore.Mvc.Internal.ActionSelector[2]
      Action 'TodoApi.Controllers.TodoController.Delete (TodoApi)' with id 'f3476abe-4bd9-4ad3-9261-3ead09607366' did not match the constraint 'Microsoft.AspNetCore.Mvc.Internal.HttpMethodActionConstraint'
dbug: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[1]
      Executing action TodoApi.Controllers.TodoController.GetById (TodoApi)
info: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[1]
      Executing action method TodoApi.Controllers.TodoController.GetById (TodoApi) with arguments (0) - ModelState is Valid
info: TodoApi.Controllers.TodoController[1002]
      Getting item 0
warn: TodoApi.Controllers.TodoController[4000]
      GetById(0) NOT FOUND
dbug: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[2]
      Executed action method TodoApi.Controllers.TodoController.GetById (TodoApi), returned result Microsoft.AspNetCore.Mvc.NotFoundResult.
info: Microsoft.AspNetCore.Mvc.StatusCodeResult[1]
      Executing HttpStatusCodeResult, setting HTTP status code 404
info: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[2]
      Executed action TodoApi.Controllers.TodoController.GetById (TodoApi) in 0.8788ms
dbug: Microsoft.AspNetCore.Server.Kestrel[9]
      Connection id "0HL6L7NEFF2QD" completed keep alive response.
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 2.7286ms 404
```

## Log event ID

Each time you write a log, you can specify an *event ID*. The sample app does this by using a locally-defined `LoggingEvents` class:

[!code-csharp[](index/sample//Controllers/TodoController.cs?name=snippet_CallLogMethods&highlight=3,7)]

[!code-csharp[](index/sample//Core/LoggingEvents.cs?name=snippet_LoggingEvents)]

An event ID is an integer value that you can use to associate a set of logged events with one another. For instance, a log for adding an item to a shopping cart could be event ID 1000 and a log for completing a purchase could be event ID 1001.

In logging output, the event ID may be stored in a field or included in the text message, depending on the provider. The Debug provider doesn't show event IDs, but the console provider shows them in brackets after the category:

```console
info: TodoApi.Controllers.TodoController[1002]
      Getting item invalidid
warn: TodoApi.Controllers.TodoController[4000]
      GetById(invalidid) NOT FOUND
```

## Log message template

Each time you write a log message, you provide a message template. The message template can be a string or it can contain named placeholders into which argument values are placed. The template isn't a format string, and placeholders should be named, not numbered.

[!code-csharp[](index/sample//Controllers/TodoController.cs?name=snippet_CallLogMethods&highlight=3,7)]

The order of placeholders, not their names, determines which parameters are used to provide their values. If you have the following code:

```csharp
string p1 = "parm1";
string p2 = "parm2";
_logger.LogInformation("Parameter values: {p2}, {p1}", p1, p2);
```

The resulting log message looks like this:

```
Parameter values: parm1, parm2
```

The logging framework does message formatting in this way to make it possible for logging providers to implement [semantic logging, also known as structured logging](https://softwareengineering.stackexchange.com/questions/312197/benefits-of-structured-logging-vs-basic-logging). Because the arguments themselves are passed to the logging system, not just the formatted message template, logging providers can store the parameter values as fields in addition to the message template. If you're directing your log output to Azure Table Storage and your logger method call looks like this:

```csharp
_logger.LogInformation("Getting item {ID} at {RequestTime}", id, DateTime.Now);
```

Each Azure Table entity can have `ID` and `RequestTime` properties, which simplifies queries on log data. You can find all logs within a particular `RequestTime` range without the need to parse the time out of the text message.

## Logging exceptions

The logger methods have overloads that let you pass in an exception, as in the following example:

[!code-csharp[](index/sample//Controllers/TodoController.cs?name=snippet_LogException&highlight=3)]

Different providers handle the exception information in different ways. Here's an example of Debug provider output from the code shown above.

```
TodoApi.Controllers.TodoController:Warning: GetById(036dd898-fb01-47e8-9a65-f92eb73cf924) NOT FOUND

System.Exception: Item not found exception.
 at TodoApi.Controllers.TodoController.GetById(String id) in C:\logging\sample\src\TodoApi\Controllers\TodoController.cs:line 226
```

## Log filtering

# [ASP.NET Core 2.x](#tab/aspnetcore2x/)

You can specify a minimum log level for a specific provider and category or for all providers or all categories. Any logs below the minimum level aren't passed to that provider, so they don't get displayed or stored. 

If you want to suppress all logs, you can specify `LogLevel.None` as the minimum log level. The integer value of `LogLevel.None` is 6, which is higher than `LogLevel.Critical` (5).

**Create filter rules in configuration**

The project templates create code that calls `CreateDefaultBuilder` to set up logging for the Console and Debug providers. The `CreateDefaultBuilder` method also sets up logging to look for configuration in a `Logging` section, using code like the following:

[!code-csharp[](index/sample2/Program.cs?name=snippet_ExpandDefault&highlight=15)]

The configuration data specifies minimum log levels by provider and category, as in the following example:

[!code-json[](index/sample2/appsettings.json)]

This JSON creates six filter rules, one for the Debug provider, four for the Console provider, and one that applies to all providers. You'll see later how just one of these rules is chosen for each provider when an `ILogger` object is created.

**Filter rules in code**

You can register filter rules in code, as shown in the following example:

[!code-csharp[](index/sample2/Program.cs?name=snippet_FilterInCode&highlight=4-5)]

The second `AddFilter` specifies the Debug provider by using its type name. The first `AddFilter` applies to all providers because it doesn't specify a provider type.

**How filtering rules are applied**

The configuration data and the `AddFilter` code shown in the preceding examples create the rules shown in the following table. The first six come from the configuration example and the last two come from the code example.

| Number | Provider      | Categories that begin with ...          | Minimum log level |
| :----: | ------------- | --------------------------------------- | ----------------- |
| 1      | Debug         | All categories                          | Information       |
| 2      | Console       | Microsoft.AspNetCore.Mvc.Razor.Internal | Warning           |
| 3      | Console       | Microsoft.AspNetCore.Mvc.Razor.Razor    | Debug             |
| 4      | Console       | Microsoft.AspNetCore.Mvc.Razor          | Error             |
| 5      | Console       | All categories                          | Information       |
| 6      | All providers | All categories                          | Debug             |
| 7      | All providers | System                                  | Debug             |
| 8      | Debug         | Microsoft                               | Trace             |

When you create an `ILogger` object to write logs with, the `ILoggerFactory` object selects a single rule per provider to apply to that logger. All messages written by that `ILogger` object are filtered based on the selected rules. The most specific rule possible for each provider and category pair is selected from the available rules.

The following algorithm is used for each provider when an `ILogger` is created for a given category:

* Select all rules that match the provider or its alias. If none are found, select all rules with an empty provider.
* From the result of the preceding step, select rules with longest matching category prefix. If none are found, select all rules that don't specify a category.
* If multiple rules are selected take the **last** one.
* If no rules are selected, use `MinimumLevel`.

For example, suppose you have the preceding list of rules and you create an `ILogger` object for category "Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine":

* For the Debug provider, rules 1, 6, and 8 apply. Rule 8 is most specific, so that's the one selected.
* For the Console provider, rules 3, 4, 5, and 6 apply. Rule 3 is most specific.

When you create logs with an `ILogger` for category "Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine", logs of `Trace` level and above will go to the Debug provider, and logs of `Debug` level and above will go to the Console provider.

**Provider aliases**

You can use the type name to specify a provider in configuration, but each provider defines a shorter *alias* that's easier to use. For the built-in providers, use the following aliases:

- Console
- Debug
- EventLog
- AzureAppServices
- TraceSource
- EventSource

**Default minimum level**

There's a minimum level setting that takes effect only if no rules from configuration or code apply for a given provider and category. The following example shows how to set the minimum level:

[!code-csharp[](index/sample2/Program.cs?name=snippet_MinLevel&highlight=3)]

If you don't explicitly set the minimum level, the default value is `Information`, which means that `Trace` and `Debug` logs are ignored.

**Filter functions**

You can write code in a filter function to apply filtering rules. A filter function is invoked for all providers and categories that don't have rules assigned to them by configuration or code. Code in the function has access to the provider type, category, and log level to decide whether or not a message should be logged. For example:

[!code-csharp[](index/sample2/Program.cs?name=snippet_FilterFunction&highlight=5-13)]

# [ASP.NET Core 1.x](#tab/aspnetcore1x/)

Some logging providers let you specify when logs should be written to a storage medium or ignored based on log level and category.

The `AddConsole` and `AddDebug` extension methods provide overloads that let you pass in filtering criteria. The following sample code causes the console provider to ignore logs below `Warning` level, while the Debug provider ignores logs that the framework creates.

[!code-csharp[](index/sample/Startup.cs?name=snippet_AddConsoleAndDebugWithFilter&highlight=6-7)]

The `AddEventLog` method has an overload that takes an `EventLogSettings` instance, which may contain a filtering function in its `Filter` property. The TraceSource provider doesn't provide any of those overloads, since its logging level and other parameters are based on the `SourceSwitch` and `TraceListener` it uses.

You can set filtering rules for all providers that are registered with an `ILoggerFactory` instance by using the `WithFilter` extension method. The example below limits framework logs (category begins with "Microsoft" or "System") to warnings while letting the app log at debug level.

[!code-csharp[](index/sample/Startup.cs?name=snippet_FactoryFilter&highlight=6-11)]

If you want to use filtering to prevent all logs from being written for a particular category, you can specify `LogLevel.None` as the minimum log level for that category. The integer value of `LogLevel.None` is 6, which is higher than `LogLevel.Critical` (5).

The `WithFilter` extension method is provided by the [Microsoft.Extensions.Logging.Filter](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Filter) NuGet package. The method returns a new `ILoggerFactory` instance that will filter the log messages passed to all logger providers registered with it. It doesn't affect any other `ILoggerFactory` instances, including the original `ILoggerFactory` instance.

---

## Log scopes

You can group a set of logical operations within a *scope* in order to attach the same data to each log that's created as part of that set. For example, you might want every log created as part of processing a transaction to include the transaction ID.

A scope is an `IDisposable` type that's returned by the `ILogger.BeginScope<TState>` method and lasts until it's disposed. You use a scope by wrapping your logger calls in a `using` block, as shown here:

[!code-csharp[](index/sample//Controllers/TodoController.cs?name=snippet_Scopes&highlight=4-5,13)]

The following code enables scopes for the console provider:

# [ASP.NET Core 2.x](#tab/aspnetcore2x/)

In *Program.cs*:

[!code-csharp[](index/sample2/Program.cs?name=snippet_Scopes&highlight=4)]

> [!NOTE]
> Configuring the `IncludeScopes` console logger option is required to enable scope-based logging. Configuration of `IncludeScopes` using *appsettings* configuration files will be available with the release of ASP.NET Core 2.1.

# [ASP.NET Core 1.x](#tab/aspnetcore1x/)

In *Startup.cs*:

[!code-csharp[](index/sample/Startup.cs?name=snippet_Scopes&highlight=6)]

---

Each log message includes the scoped information:

```
info: TodoApi.Controllers.TodoController[1002]
      => RequestId:0HKV9C49II9CK RequestPath:/api/todo/0 => TodoApi.Controllers.TodoController.GetById (TodoApi) => Message attached to logs created in the using block
      Getting item 0
warn: TodoApi.Controllers.TodoController[4000]
      => RequestId:0HKV9C49II9CK RequestPath:/api/todo/0 => TodoApi.Controllers.TodoController.GetById (TodoApi) => Message attached to logs created in the using block
      GetById(0) NOT FOUND
```

## Built-in logging providers

ASP.NET Core ships the following providers:

* [Console](#console)
* [Debug](#debug)
* [EventSource](#eventsource)
* [EventLog](#eventlog)
* [TraceSource](#tracesource)
* [Azure App Service](#appservice)

<a id="console"></a>
### The console provider

The [Microsoft.Extensions.Logging.Console](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Console) provider package sends log output to the console. 

# [ASP.NET Core 2.x](#tab/aspnetcore2x/)

```csharp
logging.AddConsole()
```

# [ASP.NET Core 1.x](#tab/aspnetcore1x/)

```csharp
loggerFactory.AddConsole()
```

[AddConsole overloads](/dotnet/api/microsoft.extensions.logging.consoleloggerextensions) let you pass in an a minimum log level, a filter function, and a boolean that indicates whether scopes are supported. Another option is to pass in an `IConfiguration` object, which can specify scopes support and logging levels. 

If you are considering the console provider for use in production, be aware that it has a significant impact on performance.

When you create a new project in Visual Studio, the `AddConsole` method looks like this:

```csharp
loggerFactory.AddConsole(Configuration.GetSection("Logging"));
```

This code refers to the `Logging` section of the *appSettings.json* file:

[!code-json[](index/sample//appsettings.json)]

The settings shown limit framework logs to warnings while allowing the app to log at debug level, as explained in the [Log filtering](#log-filtering) section. For more information, see [Configuration](xref:fundamentals/configuration/index).

---

<a id="debug"></a>
### The Debug provider

The [Microsoft.Extensions.Logging.Debug](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Debug) provider package writes log output by using the [System.Diagnostics.Debug](/dotnet/api/system.diagnostics.debug) class (`Debug.WriteLine` method calls).

On Linux, this provider writes logs to */var/log/message*.

# [ASP.NET Core 2.x](#tab/aspnetcore2x)

```csharp
logging.AddDebug()
```

# [ASP.NET Core 1.x](#tab/aspnetcore1x)

```csharp
loggerFactory.AddDebug()
```

[AddDebug overloads](/dotnet/api/microsoft.extensions.logging.debugloggerfactoryextensions) let you pass in a minimum log level or a filter function.

---

<a id="eventsource"></a>
### The EventSource provider

For apps that target ASP.NET Core 1.1.0 or higher, the [Microsoft.Extensions.Logging.EventSource](https://www.nuget.org/packages/Microsoft.Extensions.Logging.EventSource) provider package can implement event tracing. On Windows, it uses [ETW](https://msdn.microsoft.com/library/windows/desktop/bb968803). The provider is cross-platform, but there are no event collection and display tools yet for Linux or macOS. 

# [ASP.NET Core 2.x](#tab/aspnetcore2x)

```csharp
logging.AddEventSourceLogger()
```

# [ASP.NET Core 1.x](#tab/aspnetcore1x)

```csharp
loggerFactory.AddEventSourceLogger()
```

---

A good way to collect and view logs is to use the [PerfView utility](https://www.microsoft.com/download/details.aspx?id=28567). There are other tools for viewing ETW logs, but PerfView provides the best experience for working with the ETW events emitted by ASP.NET. 

To configure PerfView for collecting events logged by this provider, add the string `*Microsoft-Extensions-Logging` to the **Additional Providers** list. (Don't miss the asterisk at the start of the string.)

![Perfview Additional Providers](index/_static/perfview-additional-providers.png)

<a id="eventlog"></a>
### The Windows EventLog provider

The [Microsoft.Extensions.Logging.EventLog](https://www.nuget.org/packages/Microsoft.Extensions.Logging.EventLog) provider package sends log output to the Windows Event Log.

# [ASP.NET Core 2.x](#tab/aspnetcore2x)

```csharp
logging.AddEventLog()
```

# [ASP.NET Core 1.x](#tab/aspnetcore1x)

```csharp
loggerFactory.AddEventLog()
```

[AddEventLog overloads](/dotnet/api/microsoft.extensions.logging.eventloggerfactoryextensions) let you pass in `EventLogSettings` or a minimum log level.

---

<a id="tracesource"></a>
### The TraceSource provider

The [Microsoft.Extensions.Logging.TraceSource](https://www.nuget.org/packages/Microsoft.Extensions.Logging.TraceSource) provider package uses the [System.Diagnostics.TraceSource](/dotnet/api/system.diagnostics.tracesource) libraries and providers.

# [ASP.NET Core 2.x](#tab/aspnetcore2x)

```csharp
logging.AddTraceSource(sourceSwitchName);
```

# [ASP.NET Core 1.x](#tab/aspnetcore1x)

```csharp
loggerFactory.AddTraceSource(sourceSwitchName);
```

---

[AddTraceSource overloads](/dotnet/api/microsoft.extensions.logging.tracesourcefactoryextensions) let you pass in a source switch and a trace listener.

To use this provider, an application has to run on the .NET Framework (rather than .NET Core). The provider lets you route messages to a variety of [listeners](/dotnet/framework/debug-trace-profile/trace-listeners), such as the [TextWriterTraceListener](/dotnet/api/system.diagnostics.textwritertracelistenerr) used in the sample application.

The following example configures a `TraceSource` provider that logs `Warning` and higher messages to the console window.

[!code-csharp[](index/sample/Startup.cs?name=snippet_TraceSource&highlight=9-12)]

<a id="appservice"></a>
### The Azure App Service provider

The [Microsoft.Extensions.Logging.AzureAppServices](https://www.nuget.org/packages/Microsoft.Extensions.Logging.AzureAppServices) provider package writes logs to text files in an Azure App Service app's file system and to [blob storage](https://azure.microsoft.com/documentation/articles/storage-dotnet-how-to-use-blobs/#what-is-blob-storage) in an Azure Storage account. The provider is available only for apps that target ASP.NET Core 1.1.0 or higher. 

# [ASP.NET Core 2.x](#tab/aspnetcore2x)

If targeting .NET Core, you don't have to install the provider package or explicitly call `AddAzureWebAppDiagnostics`. The provider is automatically available to your app when you deploy the app to Azure App Service.

If targeting .NET Framework, add the provider package to your project and invoke `AddAzureWebAppDiagnostics`:

```csharp
logging.AddAzureWebAppDiagnostics();
```

# [ASP.NET Core 1.x](#tab/aspnetcore1x)

```csharp
loggerFactory.AddAzureWebAppDiagnostics();
```

An `AddAzureWebAppDiagnostics` overload lets you pass in [AzureAppServicesDiagnosticsSettings](https://github.com/aspnet/Logging/blob/c7d0b1b88668ff4ef8a86ea7d2ebb5ca7f88d3e0/src/Microsoft.Extensions.Logging.AzureAppServices/AzureAppServicesDiagnosticsSettings.cs) with which you can override default settings such as the logging output template, blob name, and file size limit. (*Output template* is a message template that's applied to all logs on top of the one that you provide when you call an `ILogger` method.)

---

When you deploy to an App Service app, your application honors the settings in the [Diagnostic Logs](https://azure.microsoft.com/documentation/articles/web-sites-enable-diagnostic-log/#enablediag) section of the **App Service** page of the Azure portal. When you change those settings, the changes take effect immediately without requiring that you restart the app or redeploy code to it. 

![Azure logging settings](index/_static/azure-logging-settings.png)

The default location for log files is in the *D:\\home\\LogFiles\\Application* folder, and the default file name is *diagnostics-yyyymmdd.txt*. The default file size limit is 10 MB, and the default maximum number of files retained is 2. The default blob name is *{app-name}{timestamp}/yyyy/mm/dd/hh/{guid}-applicationLog.txt*. For more information about default behavior, see [AzureAppServicesDiagnosticsSettings](https://github.com/aspnet/Logging/blob/c7d0b1b88668ff4ef8a86ea7d2ebb5ca7f88d3e0/src/Microsoft.Extensions.Logging.AzureAppServices/AzureAppServicesDiagnosticsSettings.cs).

The provider only works when your project runs in the Azure environment. It has no effect when you run locally &mdash; it doesn't write to local files or local development storage for blobs.

## Third-party logging providers

Third-party logging frameworks that work with ASP.NET Core:

* [elmah.io](https://elmah.io/) ([GitHub repo](https://github.com/elmahio/Elmah.Io.Extensions.Logging))
* [JSNLog](http://jsnlog.com/) ([GitHub repo](https://github.com/mperdeck/jsnlog))
* [Loggr](http://loggr.net/) ([GitHub repo](https://github.com/imobile3/Loggr.Extensions.Logging))
* [NLog](http://nlog-project.org/) ([GitHub repo](https://github.com/NLog/NLog.Extensions.Logging))
* [Serilog](https://serilog.net/) ([GitHub repo](https://github.com/serilog/serilog-extensions-logging))

Some third-party frameworks can perform [semantic logging, also known as structured logging](https://softwareengineering.stackexchange.com/questions/312197/benefits-of-structured-logging-vs-basic-logging).

Using a third-party framework is similar to using one of the built-in providers:

1. Add a NuGet package to your project.
1. Call an extension method on `ILoggerFactory`.

For more information, see each framework's documentation.

## Azure log streaming

Azure log streaming enables you to view log activity in real time from: 

* The application server 
* The web server
* Failed request tracing 

To configure Azure log streaming: 

* Navigate to the **Diagnostics Logs** page from your application's portal page
* Set **Application Logging (Filesystem)** to on. 

![Azure portal diagnostic logs page](index/_static/azure-diagnostic-logs.png)

Navigate to the **Log Streaming** page to view application messages. They're logged by application through the `ILogger` interface. 

![Azure portal application log streaming](index/_static/azure-log-streaming.png)


## See also

[High-performance logging with LoggerMessage](xref:fundamentals/logging/loggermessage)
