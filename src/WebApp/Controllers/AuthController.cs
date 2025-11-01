using System.Security.Claims;
using BuildingBlocks.Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modules.IAM.Application.Authentication.Command.ActivateNewUser;
using Modules.IAM.Application.Authentication.Command.RegisterNewUser;
using Modules.IAM.Application.Authentication.Queries.LoginQuery;
using Modules.IAM.Application.Authentication.Queries.ResendActivationToken;
using Modules.IAM.Domain.Common.Errors;
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

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var loginResult = await _mediator.Send(new LoginQuery(loginViewModel.EmailOrUserName, loginViewModel.Password));
        if (loginResult.IsFailed && loginResult.Errors.First() is UserNotOnboardedError)
        {
            return View();
        }

        if (loginResult.IsFailed)
        {
            ViewData["ErrorMessage"] = ((DomainError)loginResult.Errors.First()).Message;
            return View(loginViewModel);
        }

        var userData = loginResult.Value;
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userData.UserId.ToString()),
            new Claim(ClaimTypes.Email, userData.Email),
            new Claim(ClaimTypes.Name, userData.UserName),
            new Claim(ClaimTypes.Role, userData.Role),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = loginViewModel.RememberMe
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

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
        return RedirectToAction(nameof(RegisterSuccess), new { userId = vm.UserId });
    }

    public async Task<IActionResult> Activate(Guid userId, string token)
    {
        var result = await _mediator.Send(new ActivateNewUserCommand(userId, token));
        if (result.IsFailed)
        {
            return RedirectToAction(nameof(Register));
        }

        return View("ActivateSuccess");
    }
}