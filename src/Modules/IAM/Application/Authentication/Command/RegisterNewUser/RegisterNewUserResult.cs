using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Application.Authentication.Command.RegisterNewUser;

public record RegisterNewUserResult(UserAccount UserAccount);