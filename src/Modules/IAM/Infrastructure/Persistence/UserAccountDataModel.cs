namespace Modules.IAM.Infrastructure.Persistence;

public record UserAccountDataModel(
    Guid UserAccountId,
    string UserName,
    string Email,
    string PasswordHash,
    string Status,
    string? ActivationToken,
    string? FirstName,
    string? LastName,
    string? Avatar,
    string? Bio,
    Guid? UserPrivacySetting,
    string Role
);