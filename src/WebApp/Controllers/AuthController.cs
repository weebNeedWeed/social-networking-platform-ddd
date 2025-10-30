using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Auth;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public IActionResult Register(RegisterViewModel registerViewModel)
    {
        Console.WriteLine(registerViewModel.ToString());
        return View();
    }
}