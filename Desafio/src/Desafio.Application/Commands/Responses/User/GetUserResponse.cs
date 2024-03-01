namespace Desafio.Application;

public class GetUserResponse
{
    public IEnumerable<GetAllUserResponse> UserResponses { get; set; } = default!;
}
