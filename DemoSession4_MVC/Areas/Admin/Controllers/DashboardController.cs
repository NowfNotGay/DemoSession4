using Microsoft.AspNetCore.Mvc;

namespace DemoSession4_MVC.Areas.Admin.Controllers;
[Area("admin")]
[Route("admin/invoice")]
public class InvoiceController : Controller
{
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }
}
