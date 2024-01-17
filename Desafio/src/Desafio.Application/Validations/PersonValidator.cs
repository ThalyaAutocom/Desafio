using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class PersonValidator : AbstractValidator<Person>
{
    private readonly IPersonService _personService;

    public PersonValidator(IPersonService personService)
    {
        _personService = personService;
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Nome é obrigatório.");
        RuleFor(x => x.City).NotEmpty().NotNull().WithMessage("Cidade é obrigatória.");
        RuleFor(x => x.AlternativeCode)
            .NotNull()
            .MustAsync(UniqueAlternativeCodeAsync).WithMessage("Código Alternativo não pode se repetir.");
        RuleFor(x => x.Document)
            .NotEmpty()
            .MustAsync(UniqueDocumentAsync).WithMessage("Documento não pode se repetir.");
        RuleFor(x => x.Document).IsValidCNPJ().Unless(x => x.Document.Equals("") || x.Document.Length <= 11);
        RuleFor(x => x.Document).IsValidCPF().Unless(x => x.Document.Equals("") || x.Document.Length > 11);

    }
    private async Task<bool> UniqueAlternativeCodeAsync(string alternativeCode, CancellationToken token)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(alternativeCode)) return true;

        //Verificar se existe o código alternativo sendo usado em outro cadastro
        return !await _personService.AlternativeCodeAlreadyExistsAsync(alternativeCode);
    }
    private async Task<bool> UniqueDocumentAsync(string document, CancellationToken token)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(document)) return true;

        //Verificar se existe o documento alternativo sendo usado em outro cadastro
        return !await _personService.DocumentAlreadyExistsAsync(document);
    }
}
