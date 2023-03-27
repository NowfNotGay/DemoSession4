using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoSession4_MVC.Controllers;
[Route("Demo")]
public class DemoController : Controller
{
    private readonly IConfiguration _configuration;

    public DemoController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [Route("Index")]
    [Route("")]
    public IActionResult Index()
    {
        var value = _configuration["Config1"].ToString();
        var value2 = _configuration["Logging:LogLevel:Default"].ToString();
        Debug.WriteLine("value: "+ value);
        Debug.WriteLine("value2: " + value2);

        return View();
    }
}
