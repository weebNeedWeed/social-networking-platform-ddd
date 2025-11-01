namespace Modules.IAM.Application.Authentication.Queries.ResendActivationToken;

public class ResendActivationTokenDto
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
