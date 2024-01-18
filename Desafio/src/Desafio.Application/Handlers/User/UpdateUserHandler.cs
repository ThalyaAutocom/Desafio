using MediatR;

namespace Desafio.Application;
public class UpdateUserHandler : IRequestHandler<DeleteUserRequest, bool>
{
    private readonly IUserService _userService;

    public UpdateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        //var result = await _userService.RemoveAsync(request);

        return false;
    }
}

