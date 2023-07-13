using Microsoft.AspNetCore.Mvc;

namespace DemoSession4_MVC.Controllers;
[Route("AccessDeny")]
public class AccessDenyController : Controller
{
    [Route("")]
    [Route("index")]
    public IActionResult Index()
    {
        return View();
    }
}
