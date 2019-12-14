---
title: Configure Identity primary key data type in ASP.NET Core
author: AdrienTorris
description: Learn about the steps for configuring the desired data type used for the ASP.NET Core Identity primary key.
manager: wpickett
ms.author: scaddie
ms.date: 09/28/2017
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: article
uid: security/authentication/identity-primary-key-configuration
---
# Configure Identity primary key data type in ASP.NET Core

ASP.NET Core Identity allows you to configure the data type used to represent a primary key. Identity uses the `string` data type by default. You can override this behavior.

## Customize the primary key data type

1. Create a custom implementation of the [IdentityUser](/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuser-1) class. It represents the type to be used for creating user objects. In the following example, the default `string` type is replaced with `Guid`.

    [!code-csharp[](identity/sample/src/ASPNET-IdentityDemo-PrimaryKeysConfig/Models/ApplicationUser.cs?highlight=4&range=7-13)]

2. Create a custom implementation of the [IdentityRole](/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityrole-1) class. It represents the type to be used for creating role objects. In the following example, the default `string` type is replaced with `Guid`.

    [!code-csharp[](identity/sample/src/ASPNET-IdentityDemo-PrimaryKeysConfig/Models/ApplicationRole.cs?highlight=3&range=7-12)]

3. Create a custom database context class. It inherits from the Entity Framework database context class used for Identity. The `TUser` and `TRole` arguments reference the custom user and role classes created in the previous step, respectively. The `Guid` data type is defined for the primary key.

    [!code-csharp[](identity/sample/src/ASPNET-IdentityDemo-PrimaryKeysConfig/Data/ApplicationDbContext.cs?highlight=3&range=9-26)]

4. Register the custom database context class when adding the Identity service in the app's startup class.

   # [ASP.NET Core 2.x](#tab/aspnetcore2x/)

   The `AddEntityFrameworkStores` method doesn't accept a `TKey` argument as it did in ASP.NET Core 1.x. The primary key's data type is inferred by analyzing the `DbContext` object.

   [!code-csharp[](identity/sample/src/ASPNETv2-IdentityDemo-PrimaryKeysConfig/Startup.cs?highlight=6-8&range=25-37)]

   # [ASP.NET Core 1.x](#tab/aspnetcore1x/)

   The `AddEntityFrameworkStores` method accepts a `TKey` argument indicating the primary key's data type.

   [!code-csharp[](identity/sample/src/ASPNET-IdentityDemo-PrimaryKeysConfig/Startup.cs?highlight=9-11&range=39-55)]

   ---

## Test the changes

Upon completion of the configuration changes, the property representing the primary key reflects the new data type. The following example demonstrates accessing the property in an MVC controller.

[!code-csharp[](identity/sample/src/ASPNET-IdentityDemo-PrimaryKeysConfig/Controllers/AccountController.cs?name=snippet_GetCurrentUserId&highlight=6)]
