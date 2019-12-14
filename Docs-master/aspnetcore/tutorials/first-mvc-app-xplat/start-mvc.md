---
title: Introduction to ASP.NET Core MVC on macOS, Linux, or Windows
author: rick-anderson
description: Learn how to get started with ASP.NET Core MVC and Visual Studio Code on macOS, Linux, and Windows
manager: wpickett
ms.author: riande
ms.date: 07/07/2017
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: get-started-article
uid: tutorials/first-mvc-app-xplat/start-mvc
---
# Introduction to ASP.NET Core MVC on macOS, Linux, or Windows

By [Rick Anderson](https://twitter.com/RickAndMSFT)

This tutorial will teach you the basics of building an ASP.NET Core MVC web app using [Visual Studio Code](https://code.visualstudio.com) (VS Code). The tutorial assumes familarity with VS Code. See [Getting started with VS Code](https://code.visualstudio.com/docs) and [Visual Studio Code help](#visual-studio-code-help) for more information. 

[!INCLUDE [consider RP](../../includes/razor.md)]

There are 3 versions of this tutorial:

* macOS: [Create an ASP.NET Core MVC app with Visual Studio for Mac](xref:tutorials/first-mvc-app-mac/start-mvc)
* Windows: [Create an ASP.NET Core MVC app with Visual Studio](xref:tutorials/first-mvc-app/start-mvc)
* macOS, Linux, and Windows: [Create an ASP.NET Core MVC app with Visual Studio Code](xref:tutorials/first-mvc-app-xplat/start-mvc) 

## Prerequisites

[!INCLUDE [](~/includes/net-core-prereqs-vscode.md)]

## Create a web app with dotnet

From a terminal, run the following commands:

```console
mkdir MvcMovie
cd MvcMovie
dotnet new mvc
```

Open the *MvcMovie* folder in Visual Studio Code (VS Code) and select the *Startup.cs* file.

- Select **Yes** to the **Warn** message "Required assets to build and debug are missing from 'MvcMovie'. Add them?"
- Select **Restore** to the **Info** message "There are unresolved dependencies".

![VS Code with Warn Required assets to build and debug are missing from 'MvcMovie'. Add them? Don't ask Again, Not Now, Yes and also Info - there are unresolved dependencies  - Restore - Close](../web-api-vsc/_static/vsc_restore.png)

Press **Debug** (F5) to build and run the program.

![running app](../first-mvc-app/start-mvc/_static/1.png)

VS Code starts the [Kestrel](xref:fundamentals/servers/kestrel) web server and runs your app. Notice that the address bar shows `localhost:5000` and not something like `example.com`. That's because `localhost` is the standard hostname for your local computer.

The default template gives you working **Home, About** and **Contact** links. The browser image above doesn't show these links. Depending on the size of your browser, you might need to click the navigation icon to show them.

![navigation icon in upper right](../first-mvc-app/start-mvc/_static/2.png)

In the next part of this tutorial, we'll learn about MVC and start writing some code.

## Visual Studio Code help

- [Getting started](https://code.visualstudio.com/docs)
- [Debugging](https://code.visualstudio.com/docs/editor/debugging)
- [Integrated terminal](https://code.visualstudio.com/docs/editor/integrated-terminal)
- [Keyboard shortcuts](https://code.visualstudio.com/docs/getstarted/keybindings#_keyboard-shortcuts-reference)

  - [macOS keyboard shortcuts](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-macos.pdf)
  - [Linux keyboard shortcuts](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-linux.pdf)
  - [Windows keyboard shortcuts](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-windows.pdf)

> [!div class="step-by-step"]
> [Next - Add a controller](adding-controller.md)
