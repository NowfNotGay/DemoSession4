using Microsoft.AspNetCore.Mvc;

namespace DemoSession4_MVC.Areas.Admin.Controllers;
[Area("admin")]
[Route("admin/dashboard")]
public class DashboardController : Controller
{
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }
}
