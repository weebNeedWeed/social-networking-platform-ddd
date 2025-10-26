using BuildingBlocks.Domain;
using Modules.IAM.Domain.UserAccount.ValueObjects;
using Modules.IAM.Domain.UserPrivacySetting.ValueObjects;

namespace Modules.IAM.Domain.UserAccount;

public class UserAccount : AggregateRoot<UserAccountId>
{
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public AccountStatus Status { get; private set; }

    // TODO: should be in Profile BC
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Avatar { get; private set; }
    public string? Bio { get; private set; }

    public UserPrivacySettingId? UserPrivacySetting { get; private set; }
    public Role? Role { get; private set; }

    private UserAccount(UserAccountId id,
        string userName,
        string email,
        string passwordHash,
        AccountStatus status,
        string? firstName,
        string? lastName,
        string? avatar,
        string? bio,
        UserPrivacySettingId? userPrivacySetting,
        Role? role) : base(id)
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
            null,
            null,
            null,
            null,
            null,
            null
        );
    }
}