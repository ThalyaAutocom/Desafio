﻿using MediatR;

namespace Desafio.Application;
public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, bool>
{
    private readonly IUserService _userService;

    public UpdateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        bool result = await _userService.UpdateAsync(request);

        return result;
    }
}

