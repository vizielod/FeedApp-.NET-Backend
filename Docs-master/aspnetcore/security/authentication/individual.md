---
title: Articles based on ASP.NET Core projects created with individual user accounts
author: rick-anderson
description: Discover articles based on ASP.NET Core projects created with individual user accounts.
manager: wpickett
ms.author: riande
ms.date: 11/30/2017
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: article
uid: security/authentication/individual
---
# Articles based on ASP.NET Core projects created with individual user accounts

ASP.NET Core Identity is included in project templates in Visual Studio with the "Individual User Accounts" option.

The authentication templates are available in .NET Core CLI with `-au Individual`:

```console
dotnet new mvc -au Individual
dotnet new webapi -au Individual
dotnet new razor -au Individual
```

The following articles show how to use the code generated in ASP.NET Core templates that use individual user accounts:

* [Two-factor authentication with SMS](xref:security/authentication/2fa)
* [Account confirmation and password recovery in ASP.NET Core](xref:security/authentication/accconfirm)
* [Create an ASP.NET Core app with user data protected by authorization](xref:security/authorization/secure-data)
