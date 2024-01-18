using Desafio.Domain;

namespace Desafio.Application;

public class GetUserResponse
{
    public IEnumerable<UserResponse> UserResponses { get; set; } = default!;
}
