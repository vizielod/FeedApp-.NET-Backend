---
title: Scaffold Identity in ASP.NET Core projects
author: rick-anderson
description: Learn how to scaffold Identity in an ASP.NET Core project.
manager: wpickett
monikerRange: '>= aspnetcore-2.1'
ms.author: riande
ms.date: 5/16/2018
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: article
uid: security/authentication/scaffold-identity
---
# Scaffold Identity in ASP.NET Core projects

By [Rick Anderson](https://twitter.com/RickAndMSFT)

ASP.NET Core 2.1 and later provides [ASP.NET Core Identity](xref:security/authentication/identity) as a [Razor Class Library](xref:mvc/razor-pages/ui-class). Applications that include Identity can apply the scaffolder to selectively add the source code contained in the Identity Razor Class Library (RCL). You might want to generate source code so you can modify the code and change the behavior. For example, you could instruct the scaffolder to generate the code used in registration. Generated code takes precedence over the same code in the Identity RCL.

Applications that do **not** include authentication can apply the scaffolder to add the RCL Identity package. You have the option of selecting Identity code to be generated.

Although the scaffolder generates most of the necessary code, you'll have to update your project to complete the process. This document explains the steps needed to complete an Identity scaffolding update.

When the Identity scaffolder is run, a *ScaffoldingReadme.txt* file is created in the project directory. The *ScaffoldingReadme.txt* file contains general instructions on what's need to complete the Identity scaffolding update. This document contains more complete instructions than the read the *ScaffoldingReadme.txt* file.

We recommend using a source control system that shows file differences and allows you to back out of changes. Inspect the changes after running the Identity scaffolder.

## Scaffold identity into an empty project

[!INCLUDE[](~/includes/scaffold-identity/id-scaffold-dlg.md)]

Add the following highlighted calls to the `Startup` class:

[!code-csharp[Main](scaffold-identity/sample/StartupEmpty.cs?name=snippet1&highlight=5,20-23)]

[!INCLUDE[](~/includes/scaffold-identity/hsts.md)]

[!INCLUDE[](~/includes/scaffold-identity/migrations.md)]

## Scaffold identity into a Razor project without existing authorization

<!--
set projNam=RPnoAuth
set projType=razor
set version=2.1.0-rc1-final

dotnet new %projType% -o %projNam%
cd %projNam%
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v %version%
dotnet restore
dotnet aspnet-codegenerator identity --useDefaultUI
dotnet ef migrations add CreateIdentitySchema
dotnet ef database update
-->

[!INCLUDE[](~/includes/scaffold-identity/id-scaffold-dlg.md)]

Identity is configured in *Areas/Identity/IdentityHostingStartup.cs*. for more information, see [IHostingStartup](xref:fundamentals/configuration/platform-specific-configuration).

[!INCLUDE[](~/includes/scaffold-identity/migrations.md)]

In the `Configure` method of the `Startup` class, call [UseAuthentication](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.authappbuilderextensions.useauthentication?view=aspnetcore-2.0#Microsoft_AspNetCore_Builder_AuthAppBuilderExtensions_UseAuthentication_Microsoft_AspNetCore_Builder_IApplicationBuilder_) after `UseStaticFiles`:

[!code-csharp[Main](scaffold-identity/sample/StartupRPnoAuth.cs?name=snippet1&highlight=29)]

[!INCLUDE[](~/includes/scaffold-identity/hsts.md)]

### Layout changes

Optional: Add the login partial (`_LoginPartial`) to the layout file:

[!code-html[Main](scaffold-identity/sample/_Layout.cshtml?highlight=37)]

## Scaffold identity into a Razor project with individual authorization

<!--
dotnet new razor -au Individual -o RPauth
cd RPauth
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v "2.1.0-rc1-final"
dotnet restore
dotnet aspnet-codegenerator identity -dc RPauth.Data.ApplicationDbContext --files Account.Register
-->

[!INCLUDE[](~/includes/scaffold-identity/id-scaffold-dlg-auth.md)]
Some Identity options are configured in *Areas/Identity/IdentityHostingStartup.cs*. For more information, see [IHostingStartup](xref:fundamentals/configuration/platform-specific-configuration).

## Scaffold identity into an MVC project without existing authorization

<!--
set projNam=MvcNoAuth
set projType=mvc
set version=2.1.0-rc1-final

dotnet new %projType% -o %projNam%
cd %projNam%
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v %version%
dotnet restore
dotnet aspnet-codegenerator identity --useDefaultUI
dotnet ef migrations add CreateIdentitySchema
dotnet ef database update
-->

[!INCLUDE[](~/includes/scaffold-identity/id-scaffold-dlg.md)]

Optional: Add the login partial (`_LoginPartial`) to the *Views/Shared/_Layout.cshtml* file:

[!code-html[Main](scaffold-identity/sample/_LayoutMvc.cshtml?highlight=37)]

* Move the *Pages/Shared/_LoginPartial.cshtml* file to *Views/Shared/_LoginPartial.cshtml*

Identity is configured in *Areas/Identity/IdentityHostingStartup.cs*. For more information, see IHostingStartup.

[!INCLUDE[](~/includes/scaffold-identity/migrations.md)]

Call [UseAuthentication](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.authappbuilderextensions.useauthentication?view=aspnetcore-2.0#Microsoft_AspNetCore_Builder_AuthAppBuilderExtensions_UseAuthentication_Microsoft_AspNetCore_Builder_IApplicationBuilder_) after `UseStaticFiles`:

[!code-csharp[Main](scaffold-identity/sample/StartupMvcNoAuth.cs?name=snippet1&highlight=23)]

[!INCLUDE[](~/includes/scaffold-identity/hsts.md)]

## Scaffold identity into an MVC project with individual authorization

<!--
dotnet new mvc -au Individual -o MvcAuth
cd MvcAuth
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v "2.1.0-rc1-final"
dotnet restore
dotnet aspnet-codegenerator identity -dc MvcAuth.Data.ApplicationDbContext --files Account.Register
-->

[!INCLUDE[](~/includes/scaffold-identity/id-scaffold-dlg-auth.md)]

Delete the *Pages/Shared* folder and the files in that folder.
