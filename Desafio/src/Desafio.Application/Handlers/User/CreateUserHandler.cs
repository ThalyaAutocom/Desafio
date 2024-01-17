using AutoMapper;
using Desafio.Domain;
using Desafio.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Desafio.Application;
public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public CreateUserHandler(IMapper mapper, AppDbContext appDbContext)
    {
        _mapper = mapper;
        _appDbContext = appDbContext;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var identityUser = _mapper.Map<User>(request);

        //inserir cliente
        //retorna resultado 

        return new CreateUserResponse
        {
            Id = Guid.NewGuid().ToString(),
            ShortId = string.Empty
        };
    }
}

