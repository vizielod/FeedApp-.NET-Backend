---
title: Update the generated pages in an ASP.NET Core app
author: rick-anderson
description: Learn how to update the generated pages in an ASP.NET Core app.
manager: wpickett
monikerRange: '>= aspnetcore-2.0'
ms.author: riande
ms.date: 08/07/2017
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: get-started-article
uid: tutorials/razor-pages/da1
---
# Update the generated pages in an ASP.NET Core app

By [Rick Anderson](https://twitter.com/RickAndMSFT)

We have a good start to the movie app, but the presentation isn't ideal. We don't want to see the time (12:00:00 AM in the image below) and **ReleaseDate** should be **Release Date** (two words).

![Movie application open in Chrome showing movie data](sql/_static/m55.png)

## Update the generated code

Open the *Models/Movie.cs* file and add the highlighted lines shown in the following code:

[!code-csharp[](razor-pages-start/sample/RazorPagesMovie/Models/MovieDate.cs?name=snippet_1&highlight=10-11)]

Right click on a red squiggly line > ** Quick Actions and Refactorings**.

  ![Contextual menu shows **> Quick Actions and Refactorings**.](da1/qa.png)

Select `using System.ComponentModel.DataAnnotations;`

  ![using System.ComponentModel.DataAnnotations at top of list](da1/da.png)

  Visual studio adds `using System.ComponentModel.DataAnnotations;`.

[!INCLUDE [model1](../../includes/RP/da2.md)]

> [!div class="step-by-step"]
> [Previous: Working with SQL Server LocalDB](xref:tutorials/razor-pages/sql)
> [Add search](xref:tutorials/razor-pages/search)
