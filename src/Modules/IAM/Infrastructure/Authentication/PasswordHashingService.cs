using Modules.IAM.Application.Common.Interfaces.Authentication;

namespace Modules.IAM.Infrastructure.Authentication;

public class PasswordHasingService : IPasswordHashingService
{
    public string HashPassword(string rawPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(rawPassword);
    }

    public bool Verity(string rawPassword, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(rawPassword, hashPassword);
    }
}