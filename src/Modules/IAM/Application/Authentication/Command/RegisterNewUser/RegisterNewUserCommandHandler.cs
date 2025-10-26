using FluentResults;
using Mediator;
using Modules.IAM.Application.Common.Interfaces;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Application.Authentication.Command.RegisterNewUser;

public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Result<RegisterNewUserResult>>
{
    private readonly IIAMUnitOfWork _iAMUnitOfWork;
    private readonly IPasswordHashingService _passwordHashingService;
public RegisterNewUserCommandHandler(IIAMUnitOfWork iAMUnitOfWork, IPasswordHashingService passwordHashingService)
    {
        _passwordHashingService = passwordHashingService;
        _iAMUnitOfWork = iAMUnitOfWork;
    }

    public async ValueTask<Result<RegisterNewUserResult>> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (await _iAMUnitOfWork.UserAccountRepository.ExistsByUserNameAsync(command.UserName))
        {
            throw new ArgumentException("Invalid UserName");
        }

        if (await _iAMUnitOfWork.UserAccountRepository.ExistsByEmailAsync(command.Email))
        {
            throw new ArgumentException("Invalid Email");
        }

        var passwordHash = _passwordHashingService.HashPassword(command.Password);

        var newUser = UserAccount.RegisterNew(
            command.UserName,
            command.Email,
            passwordHash
        );

        await _iAMUnitOfWork.UserAccountRepository.AddAsync(newUser);
        await _iAMUnitOfWork.CommitAsync();

        var result = new RegisterNewUserResult(newUser,
            newUser.ActivationToken!.Token,
            newUser.ActivationToken!.ExpiresAt);

        return Result.Ok();
    }
}