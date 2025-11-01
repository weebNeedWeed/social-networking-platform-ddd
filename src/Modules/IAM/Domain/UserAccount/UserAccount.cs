using BuildingBlocks.Domain;
using FluentResults;
using Modules.IAM.Domain.Common.Errors;
using Modules.IAM.Domain.UserAccount.ValueObjects;
using Modules.IAM.Domain.UserPrivacySetting.ValueObjects;

namespace Modules.IAM.Domain.UserAccount;

public class UserAccount : AggregateRoot<UserAccountId>
{
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public AccountStatus Status { get; private set; }
    public ActivationToken? ActivationToken { get; private set; }

    // TODO: should be in Profile BC
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Avatar { get; private set; }
    public string? Bio { get; private set; }

    public UserPrivacySettingId? UserPrivacySetting { get; private set; }
    public Role Role { get; private set; }

    private UserAccount(UserAccountId id,
        string userName,
        string email,
        string passwordHash,
        AccountStatus status,
        ActivationToken? activationToken,
        string? firstName,
        string? lastName,
        string? avatar,
        string? bio,
        UserPrivacySettingId? userPrivacySetting,
        Role role) : base(id)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        Avatar = avatar;
        Bio = bio;
        UserPrivacySetting = userPrivacySetting;
        Role = role;
        Status = status;
        ActivationToken = activationToken;
    }

    public static UserAccount RegisterNew(
        string userName,
        string email,
        string passwordHash)
    {
        return new UserAccount(
            UserAccountId.CreateUnique(),
            userName,
            email,
            passwordHash,
            AccountStatus.PendingVerification,
            ActivationToken.CreateUnique(TimeSpan.FromDays(1)),
            null,
            null,
            null,
            null,
            null,
            Role.User
        );
    }

    public static UserAccount Create(
        UserAccountId id,
        string userName,
        string email,
        string passwordHash,
        AccountStatus status,
        ActivationToken? activationToken,
        string? firstName,
        string? lastName,
        string? avatar,
        string? bio,
        UserPrivacySettingId? userPrivacySetting,
        Role role)
    {
        return new UserAccount(
            id,
            userName,
            email,
            passwordHash,
            status,
            activationToken,
            firstName,
            lastName,
            avatar,
            bio,
            userPrivacySetting,
            role
        );
    }

    public Result Activate(string token)
    {
        if (ActivationToken is null)
        {
            return Result.Fail(new ActivationTokenNotFoundError());
        }

        if (ActivationToken!.Token != token)
        {
            return Result.Fail(new InvalidActivationTokenError());
        }

        if (ActivationToken!.ExpiresAt < DateTime.UtcNow)
        {
            return Result.Fail(new ActivationTokenExpiredError());
        }

        if(Status != AccountStatus.PendingVerification) {
            return Result.Fail(new AccountAlreadyActivatedError());
        }

        Status = AccountStatus.Active;
        ActivationToken = null;

        return Result.Ok();
    }
}