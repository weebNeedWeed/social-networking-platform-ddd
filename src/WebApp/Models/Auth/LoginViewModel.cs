namespace WebApp.Models.Auth;

public class LoginViewModel
{
    public string EmailOrUserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}