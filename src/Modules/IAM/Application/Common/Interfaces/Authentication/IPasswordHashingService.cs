namespace Modules.IAM.Application.Common.Interfaces.Authentication;

public interface IPasswordHashingService
{
    string HashPassword(string rawPassword);
}