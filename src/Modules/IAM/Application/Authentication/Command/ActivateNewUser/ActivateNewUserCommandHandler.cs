using FluentResults;
using MediatR;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Modules.IAM.Domain.Common.Errors;

namespace Modules.IAM.Application.Authentication.Command.ActivateNewUser;

public class ActivateNewUserCommandHandler : IRequestHandler<ActivateNewUserCommand, Result>
{
    private readonly IIAMUnitOfWork _iAMUnitOfWork;

    public ActivateNewUserCommandHandler(IIAMUnitOfWork iAMUnitOfWork)
    {
        _iAMUnitOfWork = iAMUnitOfWork;
    }

    public async Task<Result> Handle(ActivateNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _iAMUnitOfWork.UserAccountRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            return Result.Fail(new UserAccountNotFoundError());
        }

        var activateResult = user.Activate(request.Token);
        if (activateResult.IsFailed)
        {
            return activateResult;
        }

        await _iAMUnitOfWork.UserAccountRepository.AddAsync(user);
        _iAMUnitOfWork.Commit();

        return Result.Ok();
    }
}