using MediatR;

namespace Desafio.Application;
public class GetByShortIdUserHandler : IRequestHandler<GetByShortIdUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public GetByShortIdUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserResponse> Handle(GetByShortIdUserRequest request, CancellationToken cancellationToken)
    {
        return await _userService.GetByShortIdAsync(request.ShortId);       
    }
}

