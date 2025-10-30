using FluentResults;
using FluentValidation;
using MediatR;
using Modules.IAM.Application.Common.Interfaces.Authentication;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Modules.IAM.Application.Common.Interfaces.Services;
using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Application.Authentication.Command.RegisterNewUser;

public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Result<RegisterNewUserResult>>
{
    private readonly IIAMUnitOfWork _iAMUnitOfWork;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IIAMEmailService _iAMEmailService;
    private readonly IValidator<RegisterNewUserCommand> _validator;

    public RegisterNewUserCommandHandler(IIAMUnitOfWork iAMUnitOfWork, IPasswordHashingService passwordHashingService, IIAMEmailService iAMEmailService, IValidator<RegisterNewUserCommand> validator)
    {
        _passwordHashingService = passwordHashingService;
        _iAMUnitOfWork = iAMUnitOfWork;
        _iAMEmailService = iAMEmailService;
        _validator = validator;
    }

    public async Task<Result<RegisterNewUserResult>> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
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
        _iAMUnitOfWork.Commit();

        await _iAMEmailService.SendActivationEmailAsync(newUser);
        var result = new RegisterNewUserResult(newUser);

        return Result.Ok(result);
    }
}