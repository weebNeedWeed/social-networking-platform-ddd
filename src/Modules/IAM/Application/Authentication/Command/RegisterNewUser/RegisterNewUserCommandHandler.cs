using FluentResults;
using MediatR;
using Modules.IAM.Application.Common.Interfaces.Authentication;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Modules.IAM.Application.Common.Interfaces.Services;
using Modules.IAM.Domain.Common.Errors;
using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Application.Authentication.Command.RegisterNewUser;

public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Result<RegisterNewUserResult>>
{
    private readonly IIAMUnitOfWork _iAMUnitOfWork;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IIAMEmailService _iAMEmailService;

    public RegisterNewUserCommandHandler(IIAMUnitOfWork iAMUnitOfWork, IPasswordHashingService passwordHashingService, IIAMEmailService iAMEmailService)
    {
        _passwordHashingService = passwordHashingService;
        _iAMUnitOfWork = iAMUnitOfWork;
        _iAMEmailService = iAMEmailService;
    }

    public async Task<Result<RegisterNewUserResult>> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
        if (await _iAMUnitOfWork.UserAccountRepository.ExistsByUserNameAsync(command.UserName))
        {
            return Result.Fail(new UserAlreadyExistsError());
        }

        if (await _iAMUnitOfWork.UserAccountRepository.ExistsByEmailAsync(command.Email))
        {
            return Result.Fail(new UserAlreadyExistsError());
        }

        var passwordHash = _passwordHashingService.HashPassword(command.Password);

        var newUser = UserAccount.RegisterNew(
            command.UserName,
            command.Email,
            passwordHash
        );

        await _iAMUnitOfWork.UserAccountRepository.AddAsync(newUser);
        _iAMUnitOfWork.Commit();

        await _iAMEmailService.SendActivationEmailAsync(newUser.UserName, newUser.Email, newUser.ActivationToken!.Token);
        var result = new RegisterNewUserResult(newUser);

        return Result.Ok(result);
    }
}