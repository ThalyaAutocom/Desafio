using Desafio.Domain;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UpdateLoginUserRequest
{
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }

}
