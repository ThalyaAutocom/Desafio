﻿using Desafio.Domain;
using FluentValidation;
using MediatR;

namespace Desafio.Application;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    private readonly IUserService _userService;
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("The field {PropertyName} is required.")
            .EmailAddress().WithMessage("{PropertyName} invalid.");

        RuleFor(x => x.Password)
            .NotNull().NotEmpty().WithMessage("The field {PropertyName} is required.");
    }
}