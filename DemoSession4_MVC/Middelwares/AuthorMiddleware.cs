using Azure.Identity;
using DemoSession4_MVC.Models.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DemoSession4_MVC.Middelwares;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class AuthorMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext,IAccountService _accountService)
    {
        var username = httpContext.Session.GetString("username");
        httpContext.Items["account"] = _accountService.GetAccountByUsername(username);
        return _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class AuthorMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthorMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorMiddleware>();
    }
}
