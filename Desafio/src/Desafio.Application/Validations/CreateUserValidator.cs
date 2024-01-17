using Desafio.Domain;
using FluentValidation;
using MediatR;

namespace Desafio.Application;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    private readonly IUserService _userService;
    public CreateUserValidator(IUserService userService)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.UserLevel).IsInEnum().WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.NickName)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .MustAsync(async (nickname, _) => !await _userService.NickNameAlreadyUsed(nickname)).WithMessage("The field {PropertyName} must be unique.");

        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .EmailAddress().WithMessage("Invalid E-mail.")
            .MustAsync(async (email, _) => !await userService.EmailAlreadyUsed(email)).WithMessage("The Email must be unique.");

        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Senha é obrigatória.")
            .Length(8, 20).WithMessage("The field {PropertyName} must have between {0} and {1} caracteres.")
            .Must(user => user.Any(char.IsDigit)).WithMessage("{PropertyName} must contain at least one number.")
            .Must(user => user.Any(char.IsLower)).WithMessage("{PropertyName} must contain at least one lowercase character.")
            .Must(user => user.Any(char.IsUpper)).WithMessage("{PropertyName} must contain at least one uppercase character.");

        RuleFor(x => x.Document)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .Must(document => !string.IsNullOrWhiteSpace(document) && (document.Length == 11 || document.Length == 14)).WithMessage("The field {PropertyName} is invalid.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Document).IsValidCNPJ().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 14);
                RuleFor(x => x.Document).IsValidCPF().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 11);
            });
    }
}
