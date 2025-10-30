namespace WebApp.Models.Auth;

public record RegisterViewModel(
    string UserName,
    string Email,
    string Password,
    string PasswordConfirmation
);