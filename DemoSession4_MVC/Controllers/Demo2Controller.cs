using Microsoft.AspNetCore.Mvc;

namespace DemoSession4_MVC.Controllers;
[Route("Demo2")]
public class Demo2Controller : Controller
{
    [Route("")]
    [Route("~/")]
    [Route("Index")]

    public IActionResult Index()
    {
        return View();
    }
}
