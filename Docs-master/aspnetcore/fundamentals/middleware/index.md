---
title: ASP.NET Core Middleware
author: rick-anderson
description: Learn about ASP.NET Core middleware and the request pipeline.
manager: wpickett
ms.author: riande
ms.date: 01/22/2018
ms.prod: asp.net-core
ms.technology: aspnet
ms.topic: article
uid: fundamentals/middleware/index
---
# ASP.NET Core Middleware

By [Rick Anderson](https://twitter.com/RickAndMSFT) and [Steve Smith](https://ardalis.com/)

[View or download sample code](https://github.com/aspnet/Docs/tree/master/aspnetcore/fundamentals/middleware/index/sample) ([how to download](xref:tutorials/index#how-to-download-a-sample))

## What is middleware?

Middleware is software that's assembled into an application pipeline to handle requests and responses. Each component:

* Chooses whether to pass the request to the next component in the pipeline.
* Can perform work before and after the next component in the pipeline is invoked. 

Request delegates are used to build the request pipeline. The request delegates handle each HTTP request.

Request delegates are configured using [Run](/dotnet/api/microsoft.aspnetcore.builder.runextensions), [Map](/dotnet/api/microsoft.aspnetcore.builder.mapextensions), and [Use](/dotnet/api/microsoft.aspnetcore.builder.useextensions) extension methods. An individual request delegate can be specified in-line as an anonymous method (called in-line middleware), or it can be defined in a reusable class. These reusable classes and in-line anonymous methods are *middleware*, or *middleware components*. Each middleware component in the request pipeline is responsible for invoking the next component in the pipeline, or short-circuiting the chain if appropriate.

[Migrate HTTP Modules to Middleware](xref:migration/http-modules) explains the difference between request pipelines in ASP.NET Core and ASP.NET 4.x and provides more middleware samples.

## Creating a middleware pipeline with IApplicationBuilder

The ASP.NET Core request pipeline consists of a sequence of request delegates, called one after the other, as this diagram shows (the thread of execution follows the black arrows):

![Request processing pattern showing a request arriving, processing through three middlewares, and the response leaving the application. Each middleware runs its logic and hands off the request to the next middleware at the next() statement. After the third middleware processes the request, the request passes back through the prior two middlewares in reverse order for additional processing after their next() statements before leaving the application as a response to the client.](index/_static/request-delegate-pipeline.png)

Each delegate can perform operations before and after the next delegate. A delegate can also decide to not pass a request to the next delegate, which is called short-circuiting the request pipeline. Short-circuiting is often desirable because it avoids unnecessary work. For example, the static file middleware can return a request for a static file and short-circuit the rest of the pipeline. Exception-handling delegates need to be called early in the pipeline, so they can catch exceptions that occur in later stages of the pipeline.

The simplest possible ASP.NET Core app sets up a single request delegate that handles all requests. This case doesn't include an actual request pipeline. Instead, a single anonymous function is called in response to every HTTP request.

[!code-csharp[](index/sample/Middleware/Startup.cs)]

The first [app.Run](/dotnet/api/microsoft.aspnetcore.builder.runextensions) delegate terminates the pipeline.

You can chain multiple request delegates together with [app.Use](/dotnet/api/microsoft.aspnetcore.builder.useextensions). The `next` parameter represents the next delegate in the pipeline. (Remember that you can short-circuit the pipeline by *not* calling the *next* parameter.) You can typically perform actions both before and after the next delegate, as this example demonstrates:

[!code-csharp[](index/sample/Chain/Startup.cs?name=snippet1)]

>[!WARNING]
> Don't call `next.Invoke` after the response has been sent to the client. Changes to `HttpResponse` after the response has started will throw an exception. For example, changes such as setting headers, status code, etc,  will throw an exception. Writing to the response body after calling `next`:
> - May cause a protocol violation. For example, writing more than the stated `content-length`.
> - May corrupt the body format. For example, writing an HTML footer to a CSS file.
>
> [HttpResponse.HasStarted](/dotnet/api/microsoft.aspnetcore.http.features.httpresponsefeature#Microsoft_AspNetCore_Http_Features_HttpResponseFeature_HasStarted) is a useful hint to indicate if headers have been sent and/or the body has been written to.

## Ordering

The order that middleware components are added in the `Configure` method defines the order in which they're invoked on requests, and the reverse order for the response. This ordering is critical for security, performance, and functionality.

The Configure method (shown below) adds the following middleware components:

1. Exception/error handling
2. Static file server
3. Authentication
4. MVC

# [ASP.NET Core 2.x](#tab/aspnetcore2x)


```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseExceptionHandler("/Home/Error"); // Call first to catch exceptions
                                            // thrown in the following middleware.

    app.UseStaticFiles();                   // Return static files and end pipeline.

    app.UseAuthentication();               // Authenticate before you access
                                           // secure resources.

    app.UseMvcWithDefaultRoute();          // Add MVC to the request pipeline.
}
```

# [ASP.NET Core 1.x](#tab/aspnetcore1x)

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseExceptionHandler("/Home/Error"); // Call first to catch exceptions
                                            // thrown in the following middleware.

    app.UseStaticFiles();                   // Return static files and end pipeline.

    app.UseIdentity();                     // Authenticate before you access
                                           // secure resources.

    app.UseMvcWithDefaultRoute();          // Add MVC to the request pipeline.
}
```

-----------

In the code above, `UseExceptionHandler` is the first middleware component added to the pipeline—therefore, it catches any exceptions that occur in later calls.

The static file middleware is called early in the pipeline so it can handle requests and short-circuit without going through the remaining components. The static file middleware provides **no** authorization checks. Any files served by it, including those under *wwwroot*, are publicly available. See [Static files](xref:fundamentals/static-files) for an approach to secure static files.

# [ASP.NET Core 2.x](#tab/aspnetcore2x)


If the request isn't handled by the static file middleware, it's passed on to the Identity middleware (`app.UseAuthentication`), which performs authentication. Identity doesn't short-circuit unauthenticated requests. Although Identity authenticates requests,  authorization (and rejection) occurs only after MVC selects a specific Razor Page or controller and action.

# [ASP.NET Core 1.x](#tab/aspnetcore1x)

If the request isn't handled by the static file middleware, it's passed on to the Identity middleware (`app.UseIdentity`), which performs authentication. Identity doesn't short-circuit unauthenticated requests. Although Identity authenticates requests,  authorization (and rejection) occurs only after MVC selects a specific controller and action.

-----------

The following example demonstrates a middleware ordering where requests for static files are handled by the static file middleware before the response compression middleware. Static files are not compressed with this ordering of the middleware. The MVC responses from [UseMvcWithDefaultRoute](/dotnet/api/microsoft.aspnetcore.builder.mvcapplicationbuilderextensions#Microsoft_AspNetCore_Builder_MvcApplicationBuilderExtensions_UseMvcWithDefaultRoute_Microsoft_AspNetCore_Builder_IApplicationBuilder_) can be compressed.

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseStaticFiles();         // Static files not compressed
                                  // by middleware.
    app.UseResponseCompression();
    app.UseMvcWithDefaultRoute();
}
```

<a name="middleware-run-map-use"></a>

### Use, Run, and Map

You configure the HTTP pipeline using `Use`, `Run`, and `Map`. The `Use` method can short-circuit the pipeline (that is, if it doesn't call a `next` request delegate). `Run` is a convention, and some middleware components may expose `Run[Middleware]` methods that run at the end of the pipeline.

`Map*` extensions are used as a convention for branching the pipeline. [Map](/dotnet/api/microsoft.aspnetcore.builder.mapextensions) branches the request pipeline based on matches of the given request path. If the request path starts with the given path, the branch is executed.

[!code-csharp[](index/sample/Chain/StartupMap.cs?name=snippet1)]

The following table shows the requests and responses from `http://localhost:1234` using the previous code:

| Request | Response |
| --- | --- |
| localhost:1234 | Hello from non-Map delegate.  |
| localhost:1234/map1 | Map Test 1 |
| localhost:1234/map2 | Map Test 2 |
| localhost:1234/map3 | Hello from non-Map delegate.  |

When `Map` is used, the matched path segment(s) are removed from `HttpRequest.Path` and appended to `HttpRequest.PathBase` for each request.

[MapWhen](/dotnet/api/microsoft.aspnetcore.builder.mapwhenextensions) branches the request pipeline based on the result of the given predicate. Any predicate of type `Func<HttpContext, bool>` can be used to map requests to a new branch of the pipeline. In the following example, a predicate is used to detect the presence of a query string variable `branch`:

[!code-csharp[](index/sample/Chain/StartupMapWhen.cs?name=snippet1)]

The following table shows the requests and responses from `http://localhost:1234` using the previous code:

| Request | Response |
| --- | --- |
| localhost:1234 | Hello from non-Map delegate.  |
| localhost:1234/?branch=master | Branch used = master|

`Map` supports nesting, for example:

```csharp
app.Map("/level1", level1App => {
       level1App.Map("/level2a", level2AApp => {
           // "/level1/level2a"
           //...
       });
       level1App.Map("/level2b", level2BApp => {
           // "/level1/level2b"
           //...
       });
   });
   ```

`Map` can also match multiple segments at once, for example:

 ```csharp
app.Map("/level1/level2", HandleMultiSeg);
```

## Built-in middleware

ASP.NET Core ships with the following middleware components, as well as a description of the order in which they should be added:

| Middleware | Description | Order |
| ---------- | ----------- | ----- |
| [Authentication](xref:security/authentication/identity) | Provides authentication support. | Before `HttpContext.User` is needed. Terminal for OAuth callbacks. |
| [CORS](xref:security/cors) | Configures Cross-Origin Resource Sharing. | Before components that use CORS. |
| [Diagnostics](xref:fundamentals/error-handling) | Configures diagnostics. | Before components that generate errors. |
| [ForwardedHeaders/HttpOverrides](/dotnet/api/microsoft.aspnetcore.builder.forwardedheadersextensions) | Forwards proxied headers onto the current request. | Before components that consume the updated fields (examples: Scheme, Host, ClientIP, Method). |
| [Response Caching](xref:performance/caching/middleware) | Provides support for caching responses. | Before components that require caching. |
| [Response Compression](xref:performance/response-compression) | Provides support for compressing responses. | Before components that require compression. |
| [RequestLocalization](xref:fundamentals/localization) | Provides localization support. | Before localization sensitive components. |
| [Routing](xref:fundamentals/routing) | Defines and constrains request routes. | Terminal for matching routes. |
| [Session](xref:fundamentals/app-state) | Provides support for managing user sessions. | Before components that require Session. |
| [Static Files](xref:fundamentals/static-files) | Provides support for serving static files and directory browsing. | Terminal if a request matches files. |
| [URL Rewriting ](xref:fundamentals/url-rewriting) | Provides support for rewriting URLs and redirecting requests. | Before components that consume the URL. |
| [WebSockets](xref:fundamentals/websockets) | Enables the WebSockets protocol. | Before components that are required to accept WebSocket requests. |

<a name="middleware-writing-middleware"></a>

## Writing middleware

Middleware is generally encapsulated in a class and exposed with an extension method. Consider the following middleware, which sets the culture for the current request from the query string:

[!code-csharp[](index/sample/Culture/StartupCulture.cs?name=snippet1)]

Note: The sample code above is used to demonstrate creating a middleware component. See [
Globalization and localization](xref:fundamentals/localization) for ASP.NET Core's built-in localization support.

You can test the middleware by passing in the culture, for example `http://localhost:7997/?culture=no`.

The following code moves the middleware delegate to a class:

[!code-csharp[](index/sample/Culture/RequestCultureMiddleware.cs)]

> [!NOTE]
> In ASP.NET Core 1.x, the middleware `Task` method's name must be `Invoke`. In ASP.NET Core 2.0 or later, the name can be either `Invoke` or `InvokeAsync`.

The following extension method exposes the middleware through [IApplicationBuilder](/dotnet/api/microsoft.aspnetcore.builder.iapplicationbuilder):

[!code-csharp[](index/sample/Culture/RequestCultureMiddlewareExtensions.cs)]

The following code calls the middleware from `Configure`:

[!code-csharp[](index/sample/Culture/Startup.cs?name=snippet1&highlight=5)]

Middleware should follow the [Explicit Dependencies Principle](http://deviq.com/explicit-dependencies-principle/) by exposing its dependencies in its constructor. Middleware is constructed once per *application lifetime*. See *Per-request dependencies* below if you need to share services with middleware within a request.

Middleware components can resolve their dependencies from dependency injection through constructor parameters. [`UseMiddleware<T>`](/dotnet/api/microsoft.aspnetcore.builder.usemiddlewareextensions#methods_summary) can also accept additional parameters directly.

### Per-request dependencies

Because middleware is constructed at app startup, not per-request, *scoped* lifetime services used by middleware constructors are not  shared with other dependency-injected types during each request. If you must share a *scoped* service between your middleware and other types, add these services to the `Invoke` method's signature. The `Invoke` method can accept additional parameters that are populated by dependency injection. For example:

```csharp
public class MyMiddleware
{
    private readonly RequestDelegate _next;

    public MyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IMyScopedService svc)
    {
        svc.MyProperty = 1000;
        await _next(httpContext);
    }
}
```

## Additional resources

* [Migrate HTTP Modules to Middleware](xref:migration/http-modules)
* [Application Startup](xref:fundamentals/startup)
* [Request Features](xref:fundamentals/request-features)
* [Factory-based middleware activation](xref:fundamentals/middleware/extensibility)
* [Middleware activation with a third-party container](xref:fundamentals/middleware/extensibility-third-party-container)
