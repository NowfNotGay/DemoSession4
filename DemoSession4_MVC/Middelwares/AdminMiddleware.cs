using DemoSession4_MVC.Models.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DemoSession4_MVC.Middelwares;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class AdminMiddleware
{
    private readonly RequestDelegate _next;

    public AdminMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext,IAccountService _accountService)
    {
        var url = httpContext.Request.Path.ToString().ToLower();
        if (url.StartsWith("/admin"))
        {
            if(httpContext.Session.GetString("username") == null)
            {
                httpContext.Response.Redirect("/account/login");
            }
        }
        await _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class AdminMiddlewareExtensions
{
    public static IApplicationBuilder UseAdminMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AdminMiddleware>();
    }
}
