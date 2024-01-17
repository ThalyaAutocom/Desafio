using MediatR;

namespace Desafio.Application;
public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUserService _userService;

    public CreateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.InsertUserAsync(request);

        return result;
    }
}

