﻿using AutoMapper;
using MediatR;

namespace Desafio.Application;
public class GetByIdUserHandler : IRequestHandler<GetByIdUserRequest, GetUserResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetByIdUserHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<GetUserResponse> Handle(GetByIdUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllAsync();

        return new GetUserResponse
        {
            UserResponses = result
        };
    }
}
