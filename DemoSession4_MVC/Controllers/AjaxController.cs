using DemoSession4_MVC.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DemoSession4_MVC.Controllers;
[Route("ajax")]
public class AjaxController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IProductService _productService;

    public AjaxController(IAccountService accountService, IProductService productService)
    {
        _accountService = accountService;
        _productService = productService;
    }

    [Route("")]
    [Route("index")]

    public IActionResult Index()
    {
        return View();
    }

    [Route("ajax1")]

    public IActionResult Ajax1()
    {

        return new JsonResult(new { 
            Msg = 123
        });
    }

    [Route("ajax2")]

    public IActionResult Ajax2(string fullName)
    {

        return new JsonResult(new
        {
            name = fullName,
        });
    }

    [Route("ajax3")]

    public IActionResult Ajax3(string username)
    {


        return new JsonResult(new
        {
            msg = (_accountService.CheckAccount(username))? "Username Exists": "Username no Exists",
        });;
    }

    [Route("ajax4")]
    [HttpPost]
    public IActionResult Ajax4(string username,string password)
    {
        return new JsonResult(new
        {
            msg = (_accountService.CheckLogin(username,password)) ? "Success" : "Fails",
        }); ;
    }

    [Route("find")]
    public IActionResult Find(int product_id)
    {
        return new JsonResult(_productService.GetProductByIdSelect(product_id));
    }

    [Route("select")]
    public IActionResult Select()
    {
        return new JsonResult(_productService.GetProductsSelect());
    }
}
