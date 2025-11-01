using BuildingBlocks.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.IAM.Application.Authentication.Command.RegisterNewUser;
using Modules.IAM.Application.Authentication.Queries.ResendActivationToken;
using WebApp.Models.Auth;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

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
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var registerResult = await _mediator.Send(new RegisterNewUserCommand(
            registerViewModel.UserName, registerViewModel.Email, registerViewModel.Password
        ));

        if (registerResult.IsFailed)
        {
            ViewData["ErrorMessage"] = ((DomainError)registerResult.Errors.First()).Message;
            return View(registerViewModel);
        }

        return RedirectToAction(nameof(RegisterSuccess), new {userId = registerResult.Value.UserAccount.Id.Value.ToString()});
    }

    public IActionResult RegisterSuccess([FromQuery] string userId)
    {
        if (userId is null)
        {
            return RedirectToAction(nameof(Register));
        }

        var vm = new RegisterSuccessViewModel
        {
            UserId = userId
        };

        return View(vm);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ResendActivationEmail(RegisterSuccessViewModel vm)
    {
        if (!Guid.TryParse(vm.UserId, out Guid userId))
        {
            return RedirectToAction(nameof(Register));
        }
        await _mediator.Send(new ResendActivationTokenQuery(userId));
        return RedirectToAction(nameof(RegisterSuccess), new {userId = vm.UserId});
    }
}