namespace Desafio.Application;

public class GetPersonResponse
{
    public IEnumerable<PersonResponse> PersonResponses { get; set; } = default!;
}
