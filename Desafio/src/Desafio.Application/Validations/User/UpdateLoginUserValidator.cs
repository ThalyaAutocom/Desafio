using FluentValidation;

namespace Desafio.Application.Validations.User;

public class UpdateLoginUserValidator : AbstractValidator<UpdateLoginUserRequest>
{
    private readonly IUserService _userService;
    public UpdateLoginUserValidator(IUserService userService)
    {
        _userService = userService;

        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .EmailAddress().WithMessage("Invalid Email");

        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("The field {PropertyName} must be unique.")
            .MustAsync(async (request, currentPassword, _) => await _userService.CorrectPassword(request)).WithMessage("Incorrect Password");

        RuleFor(x => x.NewPassword)
            .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
            .Length(8, 20).WithMessage("The field {PropertyName} must have between 8 and 20 caracteres.")
            .Must(user => user.Any(char.IsDigit)).WithMessage("{PropertyName} must contain at least one number.")
            .Must(user => user.Any(char.IsLower)).WithMessage("{PropertyName} must contain at least one lowercase character.")
            .Must(user => user.Any(char.IsUpper)).WithMessage("{PropertyName} must contain at least one uppercase character.");

        RuleFor(x => x.ConfirmNewPassword).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .Equal(user => user.NewPassword).WithMessage("Password and ConfirmPassword must be the same.");
    }

}
