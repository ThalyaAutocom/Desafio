using AutoMapper;
using MediatR;

namespace Desafio.Application;
public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUserHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllAsync();

        if (request.Enable is not null)
        {
            result = result.Where(x => x.Enable == request.Enable).ToList();
        }

        if(request.UserLevel is not null)
        {
            result = result.Where(x => x.UserLevel == request.UserLevel).ToList();
        }

        return new GetUserResponse
        {
            UserResponses = result
        };
    }
}

