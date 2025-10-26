namespace Modules.IAM.Application.Common.Interfaces;

public interface IPasswordHashingService
{
    string HashPassword(string rawPassword);
}