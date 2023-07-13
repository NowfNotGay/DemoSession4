using DemoSession4_MVC.Models;
using DemoSession4_MVC.Models.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using DemoSession4_MVC.Paypal;
using System.Diagnostics;

namespace DemoSession4_MVC.Controllers;
[Route("Cart")]
public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly IConfiguration _configuration;

    public CartController(IProductService productService, IConfiguration configuration)
    {
        _productService = productService;
        _configuration = configuration;
    }

    [Route("")]
    [Route("Index")]

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("carts") != null)
        {
            var carts = JsonConvert.DeserializeObject<List<ItemCart>>(HttpContext.Session.GetString("carts"));
            ViewBag.carts = carts;
            ViewBag.total = carts.Select(i => i.ProductCart.Price * i.Quantity).Sum();
        }
        ViewBag.postUrl = _configuration["Paypal:PostUrl"];
        ViewBag.returnUrl = _configuration["Paypal:ReturnUrl"];
        ViewBag.business = _configuration["Paypal:Business"];
        return View();
    }
    [Route("add")]

    public IActionResult Add(int id)
    {
        var productCart = new ProductCart(_productService.GetProductById(id));
        if (HttpContext.Session.GetString("carts") == null)
        {
            var carts = new List<ItemCart>();
            carts.Add(new ItemCart()
            {
                ProductCart = productCart,
                Quantity = 1
            });
            HttpContext.Session.SetString("carts", JsonConvert.SerializeObject(carts));
        }
        else
        {
            var carts = JsonConvert.DeserializeObject<List<ItemCart>>(HttpContext.Session.GetString("carts"));
            if(carts.FirstOrDefault(c=>c.ProductCart.Id == id) == null)
            {
                carts.Add(new ItemCart()
                {
                    ProductCart = productCart,
                    Quantity = 1
                });
            }
            else
            {
                carts.FirstOrDefault(c => c.ProductCart.Id == id).Quantity++;
            }
            HttpContext.Session.SetString("carts", JsonConvert.SerializeObject(carts));
        }
        return RedirectToAction("index");
    }
    [Route("Remove")]
    public IActionResult Remove(int id)
    {
        var carts = JsonConvert.DeserializeObject<List<ItemCart>>(HttpContext.Session.GetString("carts"));
        carts.Remove(carts.SingleOrDefault(c => c.ProductCart.Id == id));
        HttpContext.Session.SetString("carts", JsonConvert.SerializeObject(carts));
        return RedirectToAction("Index");
    }

    [Route("Update")]
    public IActionResult Update(int[] quantities)
    {
        List<ItemCart> carts = JsonConvert.DeserializeObject<List<ItemCart>>(HttpContext.Session.GetString("carts"));

        for (var i = 0; i < carts.Count; i++)
        {
            carts[i].Quantity = quantities[i];
        }
        HttpContext.Session.SetString("carts", JsonConvert.SerializeObject(carts));
        return RedirectToAction("Index");
    }

    [Route("paid")]
    public IActionResult Paid(string tx)
    {
        var result = PDTHolder.Success(tx, _configuration, Request);
        Debug.WriteLine("Transaction Info");
        Debug.WriteLine("First Name: "+ result.PayerFirstName);
        Debug.WriteLine("Last Name: " + result.PayerLastName);
        Debug.WriteLine("Email: "+result.PayerEmail);
        Debug.WriteLine("InvoiceNumber: "+result.InvoiceNumber);
        //Luu hoa don
        
        //Huy gio hang

        //Gui email bao cho khach hang ve hoa don vua dat
        return View();
    }
}
