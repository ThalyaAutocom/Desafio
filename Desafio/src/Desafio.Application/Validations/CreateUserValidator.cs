using Desafio.Domain;
using FluentValidation;
using MediatR;

namespace Desafio.Application;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Email Invalido")
            .WithMessage("The field {PropertyName} is required.")
            .MustAsync(UniqueEmailAsync).WithMessage("The Email must be unique.");

        RuleFor(x => x.Document)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .MustAsync(UniqueDocument).WithMessage("The Document must be unique.");

        RuleFor(x => x.Document).IsValidCNPJ().Unless(x => x.Document.Length <= 11);
        RuleFor(x => x.Document).IsValidCPF().Unless(x => x.Document.Length > 11);
    }

    private async Task<bool> UniqueEmailAsync(string email, CancellationToken token)
    {
        // Verificar se existe cadastro desse e-mail
        return true;
    }
    private async Task<bool> UniqueDocument(string document, CancellationToken token)
    {
        // Verificar se existe cadastro desse documento
        return true;
    }
}
