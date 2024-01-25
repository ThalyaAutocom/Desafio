using MediatR;

namespace Desafio.Application;

public class DeleteUnitRequest : DeleteRequestBase, IRequest<bool>
{

}
