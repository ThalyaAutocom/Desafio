using Desafio.Domain;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Desafio.Application;

public abstract class ServiceBase
{
    private readonly IError _error;

    protected ServiceBase(IError error)
    {
        _error = error;
    }

    protected void Notificate(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notificate(error.ErrorMessage);
        }
    }

    protected void Notificate(IEnumerable<IdentityError> identityError)
    {
        foreach (var error in identityError)
        {
            Notificate(error.Description);
        }
    }

    protected void Notificate(string mensagem)
    {
        _error.Handle(new ErrorMessage(mensagem));
    }

    protected async Task<bool> ExecuteValidationAsync<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = await validacao.ValidateAsync(entidade);

        if (validator.IsValid) return true;

        Notificate(validator);

        return false;
    }
}