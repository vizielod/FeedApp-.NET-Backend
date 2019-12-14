---
title: Microsoft.AspNetCore.App metapackage for ASP.NET Core 2.1 and later
author: Rick-Anderson
description: The Microsoft.AspNetCore.App metapackage includes all supported ASP.NET Core and Entity Framework Core packages.
manager: wpickett
monikerRange: '>= aspnetcore-2.1'
ms.author: riande
ms.date: 09/20/2017
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: article
uid: fundamentals/metapackage-app
---
# Microsoft.AspNetCore.App metapackage for ASP.NET Core 2.1

This feature requires ASP.NET Core 2.1 and later targeting .NET Core 2.1 and later.

The [Microsoft.AspNetCore.App](https://www.nuget.org/packages/Microsoft.AspNetCore.App) [metapackage](/dotnet/core/packages#metapackages) for ASP.NET Core:

* Does not include third-party dependencies except for [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/), [Remotion.Linq](https://www.nuget.org/packages/Remotion.Linq/), and [IX-Async](https://www.nuget.org/packages/System.Interactive.Async/). These 3rd-party dependencies are deemed necessary to ensure the major frameworks features function.
* Includes all supported packages by the ASP.NET Core team except those that contain third-party dependencies (other than those previously mentioned).
* Includes all supported packages by the Entity Framework Core team except those that contain third-party dependencies (other than those previously mentioned).

All the features of ASP.NET Core 2.1 and later and Entity Framework Core 2.1 and later are included in the `Microsoft.AspNetCore.App` package. The default project templates targeting ASP.NET Core 2.1 and later use this package. We recommend applications targeting ASP.NET Core 2.1 and later and Entity Framework Core 2.1 and later use the `Microsoft.AspNetCore.App` package.

The version number of the `Microsoft.AspNetCore.App` metapackage represents the ASP.NET Core version and Entity Framework Core version.

Using the `Microsoft.AspNetCore.App` metapackage provides version restrictions that protect your app:

* If a package is included that has a transitive (not direct) dependency on a package in `Microsoft.AspNetCore.App`, and those version numbers differ, NuGet will generate an error.
* Other packages added to your app cannot change the version of packages included in `Microsoft.AspNetCore.App`.
* Version consistency ensures a reliable experience. `Microsoft.AspNetCore.App` was designed to prevent untested version combinations of related bits being used together in the same app.

Applications that use the `Microsoft.AspNetCore.App` metapackage automatically take advantage of the ASP.NET Core shared framework. When you use the `Microsoft.AspNetCore.App` metapackage, **no** assets from the referenced ASP.NET Core NuGet packages are deployed with the application &mdash; the ASP.NET Core shared framework contains these assets. The assets in the shared framework are precompiled to improve application startup time. For more information, see "shared framework" in [.NET Core distribution packaging](/dotnet/core/build/distribution-packaging).

The following *.csproj* project file references the `Microsoft.AspNetCore.App` metapackage for ASP.NET Core:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
  </ItemGroup>

</Project>

```

The preceding markup represents a typical ASP.NET Core 2.1 and later template. It doesn't specify a version number for the `Microsoft.AspNetCore.App` package reference. When the version is not specified, an [implicit](https://github.com/dotnet/core/blob/master/release-notes/1.0/sdk/1.0-rc3-implicit-package-refs.md) version is specified by the SDK, that is, `Microsoft.NET.Sdk.Web`. We recommend relying on the implicit version specified by the SDK and not explicitly setting the version number on the package reference. You can leave a GitHub comment at [Discussion for the Microsoft.AspNetCore.App implicit version](https://github.com/aspnet/Docs/issues/6430).

The implicit version is set to `major.minor.0` for portable apps. The shared framework roll-forward mechanism will run the app on the latest compatible version among the installed shared frameworks. To guarantee the same version is used in development, test, and production, ensure the same version of the shared framework is installed in all environments. For self contained apps, the implicit version number is set to the `major.minor.patch` of the shared framework bundled in the installed SDK.

Specifying a version number on the `Microsoft.AspNetCore.App` reference does **not** guarantee that version of the shared framework will be chosen. For example, suppose version "2.1.1" is specified, but "2.1.3" is installed. In that case, the app will use "2.1.3". Although not recommended, you can disable roll forward (patch and/or minor). For more information regarding dotnet host roll-forward and how to configure its behavior, see [dotnet host roll forward](https://github.com/dotnet/core-setup/blob/master/Documentation/design-docs/roll-forward-on-no-candidate-fx.md).

The `Microsoft.AspNetCore.App` [metapackage](/dotnet/core/packages#metapackages) is not a traditional package that is updated from NuGet. Similar to `Microsoft.NETCore.App`, `Microsoft.AspNetCore.App` represents a shared runtime, which has special versioning semantics handled outside of NuGet. For more information, see [Packages, metapackages and frameworks](/dotnet/core/packages).

If your application previously used `Microsoft.AspNetCore.All`, see [Migrating from Microsoft.AspNetCore.All to Microsoft.AspNetCore.App](xref:fundamentals/metapackage#migrate).
