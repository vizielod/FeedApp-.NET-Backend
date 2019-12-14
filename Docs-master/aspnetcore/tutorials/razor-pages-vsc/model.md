---
title: Add a model to an ASP.NET Core Razor Pages app with Visual Studio Code
author: rick-anderson
description: Learn how to add a model to a Razor Pages app in ASP.NET Core using Visual Studio Code.
manager: wpickett
monikerRange: '>= aspnetcore-2.0'
ms.author: riande
ms.date: 08/27/2017
ms.prod: aspnet-core
ms.technology: aspnet
ms.topic: get-started-article
uid: tutorials/razor-pages-vsc/model
---

# Add a model to an ASP.NET Core Razor Pages app with Visual Studio Code

[!INCLUDE [model1](../../includes/RP/model1.md)]

## Add a data model

* Add a folder named *Models*.
* Add a class to the *Models* folder named *Movie.cs*.
* Add the following code to the *Models/Movie.cs* file:

[!INCLUDE [model 2](../../includes/RP/model2.md)]

[!INCLUDE [model 2a](../../includes/RP/model2a.md)]

[!code-csharp[](../../tutorials/razor-pages/razor-pages-start/sample/RazorPagesMovie/Startup.cs?name=snippet_ConfigureServices2&highlight=3-6)]

Build the project to verify you don't have any errors.

### Entity Framework Core NuGet packages for migrations

The EF tools for the command-line interface (CLI) are provided in [Microsoft.EntityFrameworkCore.Tools.DotNet](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools.DotNet). To install this package, add it to the `DotNetCliToolReference` collection in the *.csproj* file. **Note:** You have to install this package by editing the *.csproj* file; you can't use the `install-package` command or the package manager GUI.

Edit the *RazorPagesMovie.csproj* file:

* Select **File** > **Open File**, and then select the *RazorPagesMovie.csproj* file.
* Add tool reference for `Microsoft.EntityFrameworkCore.Tools.DotNet` to the second **\<ItemGroup>**:

[!code-xml[](../../tutorials/razor-pages/razor-pages-start/snapshot_cli_sample/RazorPagesMovie/RazorPagesMovie.cli.csproj)]

[!INCLUDE [model 3](../../includes/RP/model3.md)]

<a name="scaffold"></a>
### Scaffold the Movie model

* Open a command window in the project directory (The directory that contains the *Program.cs*, *Startup.cs*, and *.csproj* files).
* Run the following command:

**Note: Run the following command on Windows. For MacOS and Linux, see the next command**

  ```console
  dotnet aspnet-codegenerator razorpage -m Movie -dc MovieContext -udl -outDir Pages\Movies --referenceScriptLibraries
  ```

* On MacOS and Linux, run the following command:

  ```console
  dotnet aspnet-codegenerator razorpage -m Movie -dc MovieContext -udl -outDir Pages/Movies --referenceScriptLibraries
  ```

If you get the error:
  ```
  The process cannot access the file 
 'RazorPagesMovie/bin/Debug/netcoreapp2.0/RazorPagesMovie.dll' 
  because it is being used by another process.
  ```

Exit Visual Studio and run the command again.

[!INCLUDE [model 4](../../includes/RP/model4.md)]

The next tutorial explains the files created by scaffolding.

> [!div class="step-by-step"]
> [Previous: Get Started](xref:tutorials/razor-pages-vsc/razor-pages-start)
> [Next: Scaffolded Razor Pages](xref:tutorials/razor-pages-vsc/page)
