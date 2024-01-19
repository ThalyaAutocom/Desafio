using MediatR;

namespace Desafio.Application;
public class GetByIdUserHandler : IRequestHandler<GetByIdUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public GetByIdUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserResponse> Handle(GetByIdUserRequest request, CancellationToken cancellationToken)
    {
        return await _userService.GetByIdAsync(request.Id);
    }
}

