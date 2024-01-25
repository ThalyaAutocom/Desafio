using MediatR;

namespace Desafio.Application;

public class DeleteProductRequest : DeleteRequestBase, IRequest<bool>
{

}
