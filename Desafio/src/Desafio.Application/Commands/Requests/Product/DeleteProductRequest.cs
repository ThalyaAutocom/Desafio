using MediatR;

namespace Desafio.Application;

public class DeleteProductRequest(Guid id) : IRequest<bool>
{
    public Guid Id { get; set; } = id;
}
