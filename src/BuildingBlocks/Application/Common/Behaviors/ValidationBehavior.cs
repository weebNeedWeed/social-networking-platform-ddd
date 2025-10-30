using BuildingBlocks.Domain;
using FluentResults;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResultBase
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next(cancellationToken);
        }

        var validationResult = await _validator.ValidateAsync(request);
        if (validationResult.IsValid)
        {
            return await next(cancellationToken);
        }

        var errors = validationResult.Errors.Select(error
            => new ValidationError(error.PropertyName, error.ErrorMessage)).ToList();

        return (dynamic) Result.Fail(errors);
    }
}