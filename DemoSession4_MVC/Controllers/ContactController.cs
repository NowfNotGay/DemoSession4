using DemoSession4_MVC.Helpers;
using DemoSession4_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoSession4_MVC.Controllers;
[Route("Contact")]
public class ContactController : Controller
{
    private readonly IConfiguration _configuration;

    public ContactController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [Route("Index")]
    [Route("")]
    [HttpGet]
    public IActionResult Index()
    {
        return View(new Contact());
    }

    [Route("sendmail")]
    [HttpPost]
    public IActionResult Index(Contact contact)
    {
        try
        {
            var send = new MailHelper(_configuration);
            var content = "Full Name: " + contact.FullName;
            content+= "<br/>Email: "+contact.Email;
            content += "<br/>Phone: " + contact.Phone;
            content += "<br/>Content: " + contact.Content;
            if (send.Send("vuong062125@gmail.com", "vuong062125@gmail.com", contact.Title, content))
            {
                TempData["msg"] = "Send";
            }
            else
            {
                TempData["msg"] = "Faild";

            }
        }
        catch(Exception ex)
        {
            
            TempData["msg"] = "Failed";
        }
        return RedirectToAction("index");
    }
}
