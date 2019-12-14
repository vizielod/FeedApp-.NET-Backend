---
title: Configuration in ASP.NET Core
author: rick-anderson
description: Use the Configuration API to configure an ASP.NET Core app by multiple methods.
manager: wpickett
ms.author: riande
ms.custom: mvc
ms.date: 01/11/2018
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: article
uid: fundamentals/configuration/index
---
# Configuration in ASP.NET Core

By [Rick Anderson](https://twitter.com/RickAndMSFT), [Mark Michaelis](http://intellitect.com/author/mark-michaelis/), [Steve Smith](https://ardalis.com/), [Daniel Roth](https://github.com/danroth27), and [Luke Latham](https://github.com/guardrex)

The Configuration API provides a way to configure an ASP.NET Core web app based on a list of name-value pairs. Configuration is read at runtime from multiple sources. Name-value pairs can be grouped into a multi-level hierarchy.

There are configuration providers for:

* File formats (INI, JSON, and XML).
* Command-line arguments.
* Environment variables.
* In-memory .NET objects.
* The unencrypted [Secret Manager](xref:security/app-secrets) storage.
* An encrypted user store, such as [Azure Key Vault](xref:security/key-vault-configuration).
* Custom providers (installed or created).

Each configuration value maps to a string key. There's built-in binding support to deserialize settings into a custom [POCO](https://wikipedia.org/wiki/Plain_Old_CLR_Object) object (a simple .NET class with properties).

The options pattern uses options classes to represent groups of related settings. For more information on using the options pattern, see the [Options](xref:fundamentals/configuration/options) topic.

[View or download sample code](https://github.com/aspnet/docs/tree/master/aspnetcore/fundamentals/configuration/index/sample) ([how to download](xref:tutorials/index#how-to-download-a-sample))

## JSON configuration

The following console app uses the JSON configuration provider:

[!code-csharp[](index/sample/ConfigJson/Program.cs)]

The app reads and displays the following configuration settings:

[!code-json[](index/sample/ConfigJson/appsettings.json)]

Configuration consists of a hierarchical list of name-value pairs in which the nodes are separated by a colon (`:`). To retrieve a value, access the `Configuration` indexer with the corresponding item's key:

[!code-csharp[](index/sample/ConfigJson/Program.cs?range=21-22)]

To work with arrays in JSON-formatted configuration sources, use an array index as part of the colon-separated string. The following example gets the name of the first item in the preceding `wizards` array:

```csharp
Console.Write($"{Configuration["wizards:0:Name"]}");
// Output: Gandalf
```

Name-value pairs written to the built-in [Configuration](/dotnet/api/microsoft.extensions.configuration) providers are **not** persisted. However, a custom provider that saves values can be created. See [custom configuration provider](xref:fundamentals/configuration/index#custom-config-providers).

The preceding sample uses the configuration indexer to read values. To access configuration outside of `Startup`, use the *options pattern*. For more information, see the [Options](xref:fundamentals/configuration/options) topic.

## XML configuration

To work with arrays in XML-formatted configuration sources, provide a `name` index to each element. Use the index to access the values:

```xml
<wizards>
  <wizard name="Gandalf">
    <age>1000</age>
  </wizard>
  <wizard name="Harry">
    <age>17</age>
  </wizard>
</wizards>
```

```csharp
Console.Write($"{Configuration["wizard:Harry:age"]}");
// Output: 17
```

## Configuration by environment

It's typical to have different configuration settings for different environments, for example, development, testing, and production. The `CreateDefaultBuilder` extension method in an ASP.NET Core 2.x app (or using `AddJsonFile` and `AddEnvironmentVariables` directly in an ASP.NET Core 1.x app) adds configuration providers for reading JSON files and system configuration sources:

* *appsettings.json*
* *appsettings.\<EnvironmentName>.json*
* Environment variables

ASP.NET Core 1.x apps need to call `AddJsonFile` and [AddEnvironmentVariables](/dotnet/api/microsoft.extensions.configuration.environmentvariablesextensions.addenvironmentvariables#Microsoft_Extensions_Configuration_EnvironmentVariablesExtensions_AddEnvironmentVariables_Microsoft_Extensions_Configuration_IConfigurationBuilder_System_String_).

See [AddJsonFile](/dotnet/api/microsoft.extensions.configuration.jsonconfigurationextensions) for an explanation of the parameters. `reloadOnChange` is only supported in ASP.NET Core 1.1 and later.

Configuration sources are read in the order that they're specified. In the preceding code, the environment variables are read last. Any configuration values set through the environment replace those set in the two previous providers.

Consider the following *appsettings.Staging.json* file:

[!code-json[](index/sample/appsettings.Staging.json)]

When the environment is set to `Staging`, the following `Configure` method reads the value of `MyConfig`:

[!code-csharp[](index/sample/StartupConfig.cs?name=snippet&highlight=3,4)]

The environment is typically set to `Development`, `Staging`, or `Production`. For more information, see [Use multiple environments](xref:fundamentals/environments).

Configuration considerations:

* [IOptionsSnapshot](xref:fundamentals/configuration/options#reload-configuration-data-with-ioptionssnapshot) can reload configuration data when it changes.
* Configuration keys are **not** case-sensitive.
* **Never** store passwords or other sensitive data in configuration provider code or in plain text configuration files. Don't use production secrets in development or test environments. Specify secrets outside of the project so that they can't be accidentally committed to a source code repository. Learn more about [how to use multiple environments](xref:fundamentals/environments) and managing [safe storage of app secrets in development](xref:security/app-secrets).
* For hierarchical config values specified in environment variables, a colon (`:`) may not work on all platforms. Double underscore (`__`) is supported by all platforms.
* When interacting with the configuration API, a colon (`:`) works on all platforms.

## In-memory provider and binding to a POCO class

The following sample shows how to use the in-memory provider and bind to a class:

[!code-csharp[](index/sample/InMemory/Program.cs)]

Configuration values are returned as strings, but binding enables the construction of objects. Binding allows the retrieval of POCO objects or even entire object graphs.

### GetValue

The following sample demonstrates the [GetValue&lt;T&gt;](/dotnet/api/microsoft.extensions.configuration.configurationbinder.get?view=aspnetcore-2.0#Microsoft_Extensions_Configuration_ConfigurationBinder_Get__1_Microsoft_Extensions_Configuration_IConfiguration_) extension method:

[!code-csharp[](index/sample/InMemoryGetValue/Program.cs?highlight=31)]

The ConfigurationBinder's `GetValue<T>` method allows the specification of a default value (80 in the sample). `GetValue<T>` is for simple scenarios and doesn't bind to entire sections. `GetValue<T>` obtains scalar values from `GetSection(key).Value` converted to a specific type.

## Bind to an object graph

Each object in a class can be recursively bound. Consider the following `AppSettings` class:

[!code-csharp[](index/sample/ObjectGraph/AppSettings.cs)]

The following sample binds to the `AppSettings` class:

[!code-csharp[](index/sample/ObjectGraph/Program.cs?highlight=15-16)]

**ASP.NET Core 1.1** and higher can use `Get<T>`, which works with entire sections. `Get<T>` can be more convenient than using `Bind`. The following code shows how to use `Get<T>` with the preceding sample:

```csharp
var appConfig = config.GetSection("App").Get<AppSettings>();
```

Using the following *appsettings.json* file:

[!code-json[](index/sample/ObjectGraph/appsettings.json)]

The program displays `Height 11`.

The following code can be used to unit test the configuration:

```csharp
[Fact]
public void CanBindObjectTree()
{
    var dict = new Dictionary<string, string>
        {
            {"App:Profile:Machine", "Rick"},
            {"App:Connection:Value", "connectionstring"},
            {"App:Window:Height", "11"},
            {"App:Window:Width", "11"}
        };
    var builder = new ConfigurationBuilder();
    builder.AddInMemoryCollection(dict);
    var config = builder.Build();

    var settings = new AppSettings();
    config.GetSection("App").Bind(settings);

    Assert.Equal("Rick", settings.Profile.Machine);
    Assert.Equal(11, settings.Window.Height);
    Assert.Equal(11, settings.Window.Width);
    Assert.Equal("connectionstring", settings.Connection.Value);
}
```

<a name="custom-config-providers"></a>

## Create an Entity Framework custom provider

In this section, a basic configuration provider that reads name-value pairs from a database using EF is created.

Define a `ConfigurationValue` entity for storing configuration values in the database:

[!code-csharp[](index/sample/CustomConfigurationProvider/ConfigurationValue.cs)]

Add a `ConfigurationContext` to store and access the configured values:

[!code-csharp[](index/sample/CustomConfigurationProvider/ConfigurationContext.cs?name=snippet1)]

Create a class that implements [IConfigurationSource](/dotnet/api/Microsoft.Extensions.Configuration.IConfigurationSource):

[!code-csharp[](index/sample/CustomConfigurationProvider/EntityFrameworkConfigurationSource.cs?highlight=7)]

Create the custom configuration provider by inheriting from [ConfigurationProvider](/dotnet/api/Microsoft.Extensions.Configuration.ConfigurationProvider). The configuration provider initializes the database when it's empty:

[!code-csharp[](index/sample/CustomConfigurationProvider/EntityFrameworkConfigurationProvider.cs?highlight=9,18-31,38-39)]

The highlighted values from the database ("value_from_ef_1" and "value_from_ef_2") are displayed when the sample is run.

An `EFConfigSource` extension method for adding the configuration source can be used:

[!code-csharp[](index/sample/CustomConfigurationProvider/EntityFrameworkExtensions.cs?highlight=12)]

The following code shows how to use the custom `EFConfigProvider`:

[!code-csharp[](index/sample/CustomConfigurationProvider/Program.cs?highlight=21-26)]

Note the sample adds the custom `EFConfigProvider` after the JSON provider, so any settings from the database will override settings from the *appsettings.json* file.

Using the following *appsettings.json* file:

[!code-json[](index/sample/CustomConfigurationProvider/appsettings.json)]

The following output is displayed:

```console
key1=value_from_ef_1
key2=value_from_ef_2
key3=value_from_json_3
```

## CommandLine configuration provider

The [CommandLine configuration provider](/dotnet/api/microsoft.extensions.configuration.commandline.commandlineconfigurationprovider) receives command-line argument key-value pairs for configuration at runtime.

[View or download the CommandLine configuration sample](https://github.com/aspnet/docs/tree/master/aspnetcore/fundamentals/index/sample/CommandLine)

### Setup and use the CommandLine configuration provider

# [Basic Configuration](#tab/basicconfiguration/)

To activate command-line configuration, call the `AddCommandLine` extension method on an instance of [ConfigurationBuilder](/dotnet/api/microsoft.extensions.configuration.configurationbuilder):

[!code-csharp[](index/sample_snapshot//CommandLine/Program.cs?highlight=18,21)]

Running the code, the following output is displayed:

```console
MachineName: MairaPC
Left: 1980
```

Passing argument key-value pairs on the command line changes the values of `Profile:MachineName` and `App:MainWindow:Left`:

```console
dotnet run Profile:MachineName=BartPC App:MainWindow:Left=1979
```

The console window displays:

```console
MachineName: BartPC
Left: 1979
```

To override configuration provided by other configuration providers with command-line configuration, call `AddCommandLine` last on `ConfigurationBuilder`:

[!code-csharp[](index/sample_snapshot//CommandLine/Program2.cs?range=11-16&highlight=1,5)]

# [ASP.NET Core 2.x](#tab/aspnetcore2x/)

Typical ASP.NET Core 2.x apps use the static convenience method `CreateDefaultBuilder` to build the host:

[!code-csharp[](index/sample_snapshot//Program.cs?highlight=12)]

`CreateDefaultBuilder` loads optional configuration from *appsettings.json*, *appsettings.{Environment}.json*, [user secrets](xref:security/app-secrets) (in the `Development` environment), environment variables, and command-line arguments. The CommandLine configuration provider is called last. Calling the provider last allows the command-line arguments passed at runtime to override configuration set by the other configuration providers called earlier.

For *appsettings* files where:

* `reloadOnChange` is enabled.
* Contain the same setting in the command-line arguments and an *appsettings* file.
* The *appsettings* file containing the matching command-line argument is changed after the app starts.

If all the preceding conditions are true, the command-line arguments are overridden.

ASP.NET Core 2.x app can use [WebHostBuilder](/dotnet/api/microsoft.aspnetcore.hosting.webhostbuilder) instead of `CreateDefaultBuilder`. When using `WebHostBuilder`, manually set configuration with [ConfigurationBuilder](/api/microsoft.extensions.configuration.configurationbuilder). See the ASP.NET Core 1.x tab for more information.

# [ASP.NET Core 1.x](#tab/aspnetcore1x/)

Create a [ConfigurationBuilder](/api/microsoft.extensions.configuration.configurationbuilder) and call the `AddCommandLine` method to use the CommandLine configuration provider. Calling the provider last allows the command-line arguments passed at runtime to override configuration set by the other configuration providers called earlier. Apply the configuration to [WebHostBuilder](/dotnet/api/microsoft.aspnetcore.hosting.webhostbuilder) with the `UseConfiguration` method:

[!code-csharp[](index/sample_snapshot//CommandLine/Program2.cs?highlight=11,15,19)]

---

### Arguments

Arguments passed on the command line must conform to one of two formats shown in the following table:

| Argument format                                                     | Example        |
| ------------------------------------------------------------------- | :------------: |
| Single argument: a key-value pair separated by an equals sign (`=`) | `key1=value`   |
| Sequence of two arguments: a key-value pair separated by a space    | `/key1 value1` |

**Single argument**

The value must follow an equals sign (`=`). The value can be null (for example, `mykey=`).

The key may have a prefix.

| Key prefix               | Example         |
| ------------------------ | :-------------: |
| No prefix                | `key1=value1`   |
| Single dash (`-`)&#8224; | `-key2=value2`  |
| Two dashes (`--`)        | `--key3=value3` |
| Forward slash (`/`)      | `/key4=value4`  |

&#8224;A key with a single dash prefix (`-`) must be provided in [switch mappings](#switch-mappings), described below.

Example command:

```console
dotnet run key1=value1 -key2=value2 --key3=value3 /key4=value4
```

Note: If `-key2` isn't present in the [switch mappings](#switch-mappings) given to the configuration provider, a `FormatException` is thrown.

**Sequence of two arguments**

The value can't be null and must follow the key separated by a space.

The key must have a prefix.

| Key prefix               | Example         |
| ------------------------ | :-------------: |
| Single dash (`-`)&#8224; | `-key1 value1`  |
| Two dashes (`--`)        | `--key2 value2` |
| Forward slash (`/`)      | `/key3 value3`  |

&#8224;A key with a single dash prefix (`-`) must be provided in [switch mappings](#switch-mappings), described below.

Example command:

```console
dotnet run -key1 value1 --key2 value2 /key3 value3
```

Note: If `-key1` isn't present in the [switch mappings](#switch-mappings) given to the configuration provider, a `FormatException` is thrown.

### Duplicate keys

If duplicate keys are provided, the last key-value pair is used.

### Switch mappings

When manually building configuration with `ConfigurationBuilder`, a switch mappings dictionary can be added to the `AddCommandLine` method. Switch mappings allow key name replacement logic.

When the switch mappings dictionary is used, the dictionary is checked for a key that matches the key provided by a command-line argument. If the command-line key is found in the dictionary, the dictionary value (the key replacement) is passed back to set the configuration. A switch mapping is required for any command-line key prefixed with a single dash (`-`).

Switch mappings dictionary key rules:

* Switches must start with a dash (`-`) or double-dash (`--`).
* The switch mappings dictionary must not contain duplicate keys.

In the following example, the `GetSwitchMappings` method allows command-line arguments to use a single dash (`-`) key prefix and avoid leading subkey prefixes.

[!code-csharp[](index/sample/CommandLine/Program.cs?highlight=10-19,32)]

Without providing command-line arguments, the dictionary provided to `AddInMemoryCollection` sets the configuration values. Run the app with the following command:

```console
dotnet run
```

The console window displays:

```console
MachineName: RickPC
Left: 1980
```

Use the following to pass in configuration settings:

```console
dotnet run /Profile:MachineName=DahliaPC /App:MainWindow:Left=1984
```

The console window displays:

```console
MachineName: DahliaPC
Left: 1984
```

After the switch mappings dictionary is created, it contains the data shown in the following table:

| Key            | Value                 |
| -------------- | --------------------- |
| `-MachineName` | `Profile:MachineName` |
| `-Left`        | `App:MainWindow:Left` |

To demonstrate key switching using the dictionary, run the following command:

```console
dotnet run -MachineName=ChadPC -Left=1988
```

The command-line keys are swapped. The console window displays the configuration values for `Profile:MachineName` and `App:MainWindow:Left`:

```console
MachineName: ChadPC
Left: 1988
```

## web.config file

A *web.config* file is required when hosting the app in IIS or IIS Express. Settings in *web.config* enable the [ASP.NET Core Module](xref:fundamentals/servers/aspnet-core-module) to launch the app and configure other IIS settings and modules. If the *web.config* file isn't present and the project file includes `<Project Sdk="Microsoft.NET.Sdk.Web">`, publishing the project creates a *web.config* file in the published output (the *publish* folder). For more information, see [Host ASP.NET Core on Windows with IIS](xref:host-and-deploy/iis/index#webconfig-file).

## Access configuration during startup

To access configuration within `ConfigureServices` or `Configure` during startup, see the examples in the [Application startup](xref:fundamentals/startup) topic.

## Adding configuration from an external assembly

An [IHostingStartup](/dotnet/api/microsoft.aspnetcore.hosting.ihostingstartup) implementation allows adding enhancements to an app at startup from an external assembly outside of the app's `Startup` class. For more information, see [Enhance an app from an external assembly](xref:fundamentals/configuration/platform-specific-configuration).

## Access configuration in a Razor Page or MVC view

To access configuration settings in a Razor Pages page or an MVC view, add a [using directive](xref:mvc/views/razor#using) ([C# reference: using directive](/dotnet/csharp/language-reference/keywords/using-directive)) for the [Microsoft.Extensions.Configuration namespace](/dotnet/api/microsoft.extensions.configuration) and inject [IConfiguration](/dotnet/api/microsoft.extensions.configuration.iconfiguration) into the page or view.

In a Razor Pages page:

```cshtml
@page
@model IndexModel

@using Microsoft.Extensions.Configuration
@inject IConfiguration Configuration

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Index Page</title>
</head>
<body>
    <h1>Access configuration in a Razor Pages page</h1>
    <p>Configuration[&quot;key&quot;]: @Configuration["key"]</p>
</body>
</html>
```

In an MVC view:

```cshtml
@using Microsoft.Extensions.Configuration
@inject IConfiguration Configuration

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Index View</title>
</head>
<body>
    <h1>Access configuration in an MVC view</h1>
    <p>Configuration[&quot;key&quot;]: @Configuration["key"]</p>
</body>
</html>
```

## Additional notes

* Dependency Injection (DI) isn't set up until after `ConfigureServices` is invoked.
* The configuration system isn't DI aware.
* `IConfiguration` has two specializations:
  * `IConfigurationRoot` Used for the root node. Can trigger a reload.
  * `IConfigurationSection` Represents a section of configuration values. The `GetSection` and `GetChildren` methods return an `IConfigurationSection`.
  * Use [IConfigurationRoot](/dotnet/api/microsoft.extensions.configuration.iconfigurationroot) when reloading configuration or for access to each provider. Neither of these situations are common.

## Additional resources

* [Options](xref:fundamentals/configuration/options)
* [Use multiple environments](xref:fundamentals/environments)
* [Safe storage of app secrets in development](xref:security/app-secrets)
* [Host in ASP.NET Core](xref:fundamentals/host/index)
* [Dependency Injection](xref:fundamentals/dependency-injection)
* [Azure Key Vault configuration provider](xref:security/key-vault-configuration)
