using MediatR;

namespace Desafio.Application;
public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IUserService _userService;

    public GetUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllAsync();

        if (request.Enable)
        {
            result = result.Where(x => x.Enable);
        }

        if(request.UserLevel != null)
        {
            result = result.Where(x => x.UserLevel == request.UserLevel);
        }
        return null;
    }
}

