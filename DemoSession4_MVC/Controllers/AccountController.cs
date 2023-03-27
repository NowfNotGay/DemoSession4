using DemoSession4_MVC.Models;
using DemoSession4_MVC.Models.Interface;
using DemoSession4_MVC.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace DemoSession4_MVC.Controllers;
[Route("account")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Route("login")]
    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("username") is not null)
        {
            return RedirectToAction("Index");

        }
        return View();
    }


    [Route("login")]
    [HttpPost]
    public IActionResult Login(string username,string password)
    {
        if (!_accountService.CheckLogin(username, password))
        {
            TempData["msg"] = "Fails";
            return RedirectToAction("login");
        }
        HttpContext.Session.SetString("username", username);
        TempData["msg"] = "Done";
        return RedirectToAction("Index");
    }

    [Route("Register")]
    [HttpGet]
    public IActionResult Register()
    {
        if (HttpContext.Session.GetString("username") is not null)
        {
            return RedirectToAction("Index");

        }
        return View(new Account()
        {

        });
    }

    [Route("Register")]
    [HttpPost]
    public IActionResult Register(Account account)
    {
        if (ModelState.IsValid)
        {
            if (_accountService.CheckAccount(account.Username))
            {
                ModelState.AddModelError("Username", "Username is exists");
                return View("Register");
            }
            account.Status = true;
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            if (!_accountService.AddAccount(account))
            {
                TempData["msg"] = "Fails";
                return RedirectToAction("Register");
            }
            TempData["msg"] = "Done";
            return RedirectToAction("Login");

        }
        else
        {
            TempData["msg"] = "Fails";
            return View("Register");
        }
    }

    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("username");
        return RedirectToAction("login");
    }

    [Route("Profile")]
    [HttpGet]
    public IActionResult Profile()
    {
        return View(_accountService.GetAccountByUsernameNoTracking(HttpContext.Session.GetString("username")));
    }

    [Route("Profile")]
    [HttpPost]
    public IActionResult Profile(Account account)
    {
        
        if (account.Password == null)
        {

            account.Password = _accountService.GetAccountByUsernameNoTracking(HttpContext.Session.GetString("username")).Password;

        }
        else
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
        }
        account.Username = HttpContext.Session.GetString("username");
        account.Status = true;
        account.Id = _accountService.GetAccountByUsernameNoTracking(HttpContext.Session.GetString("username")).Id;
        if (!_accountService.UpdateAccount(account))
        {
            TempData["msg"] = "Fails";
            return RedirectToAction("Profile");
        }

        return RedirectToAction("Login");
    }
}
