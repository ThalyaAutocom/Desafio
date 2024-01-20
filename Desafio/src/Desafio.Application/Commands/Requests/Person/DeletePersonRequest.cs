﻿using MediatR;

namespace Desafio.Application;

public class DeletePersonRequest(Guid id) : IRequest<bool>
{
    public Guid Id { get; set; } = id;
}
