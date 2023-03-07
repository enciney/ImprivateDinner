using ErrorOr;
using FluentValidation;
using ImprivateDinner.Application.Authentication.Commands.Register;
using ImprivateDinner.Application.Authentication.Common;
using MediatR;

namespace ImprivateDinner.Application.Common.Behaviors;

public class ValidationBehavior<TRequest,TResponse> : 
    IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse: IErrorOr
{
    private readonly IValidator<TRequest>? validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        this.validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if(validator is null)
        {
            return await next();
        }
        var validationResult = await validator.ValidateAsync(request,cancellationToken);
        if(validationResult.IsValid)
        {
            return await next();
        }
        var errors = validationResult.Errors.Select(v => Error.Validation(v.PropertyName, v.ErrorMessage)).ToList();
        return (dynamic)errors;
    }
}