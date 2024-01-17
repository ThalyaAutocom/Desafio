using Desafio.Domain;

namespace Desafio.Application;

public class GetUserResponse
{
    public IEnumerable<User> Users { get; set; }

}
