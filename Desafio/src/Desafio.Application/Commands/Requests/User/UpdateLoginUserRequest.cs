using MediatR;

namespace Desafio.Application;

public class UpdateLoginUserRequest : IRequest<bool>
{
    private string _nickName;

    public string NickName
    {
        get => _nickName;
        set => _nickName = value?.ToUpper();
    }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }

}
