using FluentValidation;

namespace ImprivateDinner.Application.Authentication.Queries.Login;
public class RegisterCommandValidator : AbstractValidator<LoginQuery>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}