using AutoMapper;
using MediatR;

namespace Desafio.Application;
public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, bool>
{
    private readonly IUserService _userService;

    public DeleteUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        return await _userService.RemoveAsync(request.ShortId);
    }
}

