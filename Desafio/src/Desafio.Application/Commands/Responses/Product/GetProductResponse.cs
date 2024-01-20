namespace Desafio.Application;

public class GetProductResponse
{
    public IEnumerable<ProductResponse> ProductResponses { get; set; } = default!;
}
