using System.Text.Json;
using Modules.IAM.Domain.UserAccount;
using Modules.IAM.Domain.UserAccount.ValueObjects;
using Modules.IAM.Domain.UserPrivacySetting.ValueObjects;
using Modules.IAM.Infrastructure.Persistence;

namespace Modules.IAM.Infrastructure.Common;

public static class AuthenticationMapperUtils 
{
    public static UserAccountDataModel ToUserAccountDataModel(this UserAccount userAccount)
    {
        return new UserAccountDataModel(
            UserAccountId: userAccount.Id.Value,
            UserName: userAccount.UserName,
            Email: userAccount.Email,
            PasswordHash: userAccount.PasswordHash,
            Status: userAccount.Status.Value,
            ActivationToken: userAccount.ActivationToken != null
                ? JsonSerializer.Serialize(userAccount.ActivationToken, (JsonSerializerOptions?)null)
                : null,
            FirstName: userAccount.FirstName,
            LastName: userAccount.LastName,
            Avatar: userAccount.Avatar,
            Bio: userAccount.Bio,
            UserPrivacySetting: userAccount.UserPrivacySetting?.Value,
            Role: userAccount.Role.Name
        );
    }

    public static UserAccount ToUserAccount(this UserAccountDataModel dataModel)
    {
        ActivationToken? activationToken = null;
        if (!string.IsNullOrEmpty(dataModel.ActivationToken))
        {
            var tokenData = JsonSerializer.Deserialize<ActivationTokenDto>(dataModel.ActivationToken);
            if (tokenData != null)
            {
                activationToken = ActivationToken.From(tokenData.Token, tokenData.ExpiresAt);
            }
        }

        var userPrivacySetting = dataModel.UserPrivacySetting.HasValue
            ? UserPrivacySettingId.FromValue(dataModel.UserPrivacySetting.Value)
            : null;

        return UserAccount.Create(
            id: UserAccountId.FromValue(dataModel.UserAccountId),
            userName: dataModel.UserName,
            email: dataModel.Email,
            passwordHash: dataModel.PasswordHash,
            status: AccountStatus.FromValue(dataModel.Status),
            activationToken: activationToken,
            firstName: dataModel.FirstName,
            lastName: dataModel.LastName,
            avatar: dataModel.Avatar,
            bio: dataModel.Bio,
            userPrivacySetting: userPrivacySetting,
            role: Role.FromName(dataModel.Role)
        );
    }

    private record ActivationTokenDto(string Token, DateTime ExpiresAt);
}