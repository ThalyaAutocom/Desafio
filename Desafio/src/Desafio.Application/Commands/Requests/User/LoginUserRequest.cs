using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
