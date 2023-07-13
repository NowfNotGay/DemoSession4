using DemoSession4_MVC.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace DemoSession4_MVC.Attributes;

public class MyAuthorizeAttribute: Attribute, IAuthorizationFilter
{
    public string Roles { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)   
    {
        var acc = (Account) context.HttpContext.Items["account"];
        if (acc != null)
        {
            Debug.WriteLine("Username - MyAhtorize: " + acc.Username);
            var roles = Roles.Split(new char[] { ',' });
            if (!acc.Roles.Any(role=> roles.Contains(role.Name)))
            {
                //Access Denied
                context.HttpContext.Response.Redirect("/AccessDeny");
            }
        }
    }
}
