using FluentValidation;

namespace Desafio.Application;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    private readonly IUserService _userService;
    public CreateUserValidator(IUserService userService)
    {
        _userService = userService;


        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.UserLevel).IsInEnum().WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.NickName)
            .NotNull().NotEmpty().WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.NickName)
            .MustAsync(async (nickName, _) => !await _userService.NickNameAlreadyUsed(nickName)).WithMessage("The field {PropertyName} must be unique.")
            .When(user => !string.IsNullOrWhiteSpace(user.NickName));

        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid E-mail.")
            .MustAsync(async (email, _) => !await userService.EmailAlreadyUsed(email)).WithMessage("The Email must be unique.");

        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
            .Length(8, 20).WithMessage("The field {PropertyName} must have between 8 and 20 caracteres.")
            .Must(user => user.Any(char.IsDigit)).WithMessage("{PropertyName} must contain at least one number.")
            .Must(user => user.Any(char.IsLower)).WithMessage("{PropertyName} must contain at least one lowercase character.")
            .Must(user => user.Any(char.IsUpper)).WithMessage("{PropertyName} must contain at least one uppercase character.");

        RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .Equal(user => user.Password).WithMessage("Password and ConfirmPassword must be the same.");

        RuleFor(x => x.Document)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .Must(document => !string.IsNullOrWhiteSpace(document) && (document.Length == 11 || document.Length == 14)).WithMessage("The field {PropertyName} is invalid.")
            .MustAsync(async (document, _) => !await _userService.DocumentAlreadyUsed(document)).WithMessage("The field {PropertyName} must be unique.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Document).IsValidCNPJ().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 14);
                RuleFor(x => x.Document).IsValidCPF().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 11);
            });
    }
}
