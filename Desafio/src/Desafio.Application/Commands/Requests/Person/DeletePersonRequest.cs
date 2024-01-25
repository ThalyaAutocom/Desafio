using MediatR;

namespace Desafio.Application;

public class DeletePersonRequest : DeleteRequestBase, IRequest<bool>
{
    
}
