using AutoMapper;
using MediatR;

namespace Desafio.Application;
public class GetByShortIdUserHandler : IRequestHandler<GetByShortIdUserRequest, GetUserResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetByShortIdUserHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<GetUserResponse> Handle(GetByShortIdUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllAsync();

        return new GetUserResponse
        {
            UserResponses = result
        };
    }
}

