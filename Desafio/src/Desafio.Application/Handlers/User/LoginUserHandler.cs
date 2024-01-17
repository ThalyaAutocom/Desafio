using MediatR;

namespace Desafio.Application;
public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
{
    private readonly IUserService _userService;

    public LoginUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.LoginAsync(request);
        return result;
    }
}

