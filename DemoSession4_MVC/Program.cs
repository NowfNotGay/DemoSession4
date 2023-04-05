using DemoSession4_MVC.Middelwares;
using DemoSession4_MVC.Models;
using DemoSession4_MVC.Models.Interface;
using DemoSession4_MVC.Models.Service;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddDbContext<DatabaseContext>(
    options =>
    {
        options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration["ConnectionStrings:DemoEFCore"]);
    }
);

var app = builder.Build();
app.UseSession();

//app.UseMiddleware<Log1Middleware>();
app.UseLog1Middleware();
app.UseSecurityMiddleware();
app.UseLog2Middleware();
app.UseLog3Middleware();
app.UseAdminMiddleware();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");
app.Run();
