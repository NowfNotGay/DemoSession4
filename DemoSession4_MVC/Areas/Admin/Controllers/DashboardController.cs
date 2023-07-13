using DemoSession4_MVC.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DemoSession4_MVC.Areas.Admin.Controllers;
[Area("admin")]
[Route("admin/dashboard")]
public class DashboardController : Controller
{
    [MyAuthorize(Roles = "SuperAdmin")]
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }

    [MyAuthorize(Roles = "SuperAdmin,Admin")]
    [Route("Index2")]
    public IActionResult Index2()
    {
        return View();
    }
    [MyAuthorize(Roles = "SuperAdmin,Admin,Employee")]
    [Route("Index3")]
    public IActionResult Index3()
    {
        return View();
    }

}
