namespace Modules.IAM.Application.Common.Interfaces.Authentication;

public interface IPasswordHashingService
{
    string HashPassword(string rawPassword);

    bool Verity(string rawPassword, string hashPassword);
}