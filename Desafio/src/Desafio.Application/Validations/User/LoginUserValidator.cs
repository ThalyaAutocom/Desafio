using FluentValidation;

namespace Desafio.Application.Validations.User;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.NickName).NotEmpty().WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("The field {PropertyName} is required.");
    }
}
