using MediatR;

namespace Desafio.Application;

public class DeleteUserRequest : DeleteRequestBase, IRequest<bool>
{

}
