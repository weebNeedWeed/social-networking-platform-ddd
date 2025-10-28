using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.IAM.Application.Authentication.Command.RegisterNewUser;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISender _mediator;

    public HomeController(ILogger<HomeController> logger, ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        await _mediator.Send(new RegisterNewUserCommand(
            "hello",
            "world",
            "hi"
        ));
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
