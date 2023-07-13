using DemoSession4_MVC.Helpers;
using DemoSession4_MVC.Models;
using DemoSession4_MVC.Models.Interface;
using DemoSession4_MVC.Models.Service;
using Microsoft.AspNetCore.Mvc;
using RegisterAccountAndActiveByEmail.Helpers;

namespace DemoSession4_MVC.Controllers;
[Route("account")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IConfiguration _configuration;

    public AccountController(IAccountService accountService, IConfiguration configuration)
    {
        _accountService = accountService;
        _configuration = configuration;
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
            account.Status = false;
            account.SecurityCode = RandomHelper.RandomString(6);
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            if (!_accountService.AddAccount(account))
            {
                TempData["msg"] = "Fails";
                return RedirectToAction("Register");
            }
            var content = "Sờ ciu ri ti cót: " + account.SecurityCode;
            TempData["msg"] = "Done";
            var mailhelper = new MailHelper(_configuration);
            mailhelper.Send(_configuration["Gmail:Username"],account.Email,"Verify",content);
            return RedirectToAction("verify","account", new
            {
                Email = account.Email
            });

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
    [Route("verify")]
    [HttpGet]
    public IActionResult Verify(string email)
    {
        ViewBag.email = email;
        return View();
    }

    [Route("verify")]
    [HttpPost]
    public IActionResult Verify(string securityCode, string email)
    {
        var acc = _accountService.GetAccountByEmailNoTracking(email);
        if (acc != null && acc.SecurityCode == securityCode)
        {
            acc.Status = true;
            _accountService.UpdateAccount(acc);
        }
        else
        {
            TempData["msg"]  = "Sai code";
            return RedirectToAction("verify");
        }
        return RedirectToAction("login");
    }

    [Route("forgetpassword")]
    [HttpGet]
    public IActionResult ForgetPassword()
    {
        return View();
    }

    [Route("forgetpassword")]
    [HttpPost]
    public IActionResult ForgetPassword(string email)
    {
        var acc = _accountService.GetAccountByEmailNoTracking(email);
        if(acc == null)
        {
            TempData["msg"] = "Sai email";
            return RedirectToAction("forgetpassword");
        }
        acc.SecurityCode = RandomHelper.RandomString(6);
        _accountService.UpdateAccount(acc);
        var content = "Sờ ciu ri ti cót: " + acc.SecurityCode;
        var mailhelper = new MailHelper(_configuration);
        mailhelper.Send(_configuration["Gmail:Username"], email, "Verify", content);
    
        return RedirectToAction("verify",new
        {
            Email = email
        });
    }
}
