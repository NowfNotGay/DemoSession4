﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DemoSession4_MVC.Middelwares;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class Log2Middleware
{
    private readonly RequestDelegate _next;

    public Log2Middleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        if (httpContext.Items["id"] != null)
        {
            var id = int.Parse(httpContext.Items["id"].ToString());
            Debug.WriteLine("id - log2: " + id);
        }

        if (httpContext.Items["username"] != null)
        {
            var username = httpContext.Items["username"].ToString();
            Debug.WriteLine("username - log2: " + username);
        }
        var ip = httpContext.Connection.RemoteIpAddress.ToString();
        Debug.WriteLine("Ip: " + ip);
        return _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class Log2MiddlewareExtensions
{
    public static IApplicationBuilder UseLog2Middleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<Log2Middleware>();
    }
}
