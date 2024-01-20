﻿using MediatR;

namespace Desafio.Application;

public class GetByIdProductRequest(Guid id) : IRequest<ProductResponse>
{
    public Guid Id { get; set; } = id;
}
