using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class LoginUserRequest : IRequest<LoginUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
