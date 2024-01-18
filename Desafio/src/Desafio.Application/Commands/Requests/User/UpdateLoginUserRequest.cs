using MediatR;

namespace Desafio.Application;

public class UpdateLoginUserRequest : IRequest<bool>
{
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }

}
