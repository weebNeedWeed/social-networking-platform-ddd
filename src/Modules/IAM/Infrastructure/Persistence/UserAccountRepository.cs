using System.Data;
using System.Text.Json;
using BuildingBlocks.Infrastructure.Persistence;
using Dapper;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Infrastructure.Persistence;

public class UserAccountRespository : RepositoryBase, IUserAccountRepository
{
    public UserAccountRespository(IDbTransaction transaction) : base(transaction)
    {
    }

    public async Task AddAsync(UserAccount userAccount)
    {
        var command = """
            DELETE FROM user_accounts WHERE user_account_id = @userAccountId;
        """;
        await Connection.ExecuteAsync(command, new { userAccountId = userAccount.Id.Value });

        command = """
            INSERT INTO user_accounts 
                (user_account_id, username, email, password_hash, status, activation_token, 
                first_name, last_name, avatar, bio, user_privacy_setting, role)
            VALUES 
                (@userAccountId, @userName, @email, @passwordHash, @status, @activationToken::jsonb,
                @firstName, @lastName, @avatar, @bio, @userPrivacySetting::jsonb, @role);
        """;
        var userPrivacySetting = JsonSerializer.Serialize(userAccount.UserPrivacySetting);
        var activationToken = JsonSerializer.Serialize(userAccount.ActivationToken);
        await Connection.ExecuteAsync(command, new
        {
            userAccountId = userAccount.Id.Value,
            userName = userAccount.UserName,
            email = userAccount.Email,
            passwordHash = userAccount.PasswordHash,
            status = userAccount.Status.Value,
            activationToken = activationToken,
            firstName = userAccount.FirstName,
            lastName = userAccount.LastName,
            avatar = userAccount.Avatar,
            bio = userAccount.Bio,
            userPrivacySetting = userPrivacySetting,
            role = userAccount.Role?.Name
        });
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var query = "SELECT COUNT(1) FROM user_accounts WHERE email = @email";
        var count = await Connection.ExecuteScalarAsync<int>(query, new { email = email });

        return count > 0;
    }

    public async Task<bool> ExistsByUserNameAsync(string userName)
    {
        var query = "SELECT COUNT(1) FROM user_accounts WHERE username = @username";
        var count = await Connection.ExecuteScalarAsync<int>(query, new { username = userName });

        return count > 0;
    }
}