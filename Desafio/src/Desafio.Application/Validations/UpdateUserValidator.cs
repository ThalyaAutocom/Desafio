﻿using FluentValidation;

namespace Desafio.Application;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    private readonly IUserService _userService;
    public UpdateUserValidator(IUserService userService)
    {
        _userService = userService;
        
        RuleFor(x => x.NickName)
            .MustAsync(async(nickName, _) => !await _userService.NickNameAlreadyUsed(nickName)).WithMessage("The field {PropertyName} must be unique.")
            .When(user => !string.IsNullOrWhiteSpace(user.NickName));

        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.Document)
            .Must(document => !string.IsNullOrWhiteSpace(document) && (document.Length == 11 || document.Length == 14)).WithMessage("The field {PropertyName} is invalid.")
            .MustAsync(async (document, _) => !await _userService.DocumentAlreadyUsed(document)).WithMessage("The field {PropertyName} must be unique.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Document).IsValidCNPJ().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 14);
                RuleFor(x => x.Document).IsValidCPF().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 11);
            });
    }
}