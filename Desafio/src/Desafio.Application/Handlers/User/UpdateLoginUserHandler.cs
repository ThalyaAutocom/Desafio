using MediatR;

namespace Desafio.Application;
public class UpdateLoginUserHandler : IRequestHandler<UpdateLoginUserRequest, bool>
{
    private readonly IUserService _userService;

    public UpdateLoginUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(UpdateLoginUserRequest request, CancellationToken cancellationToken)
    {
        bool result = await _userService.UpdateAsync(request);

        return result;
    }
}

