using MediatR;

namespace Desafio.Application;
public class GetByShortIdProductHandler : IRequestHandler<GetByShortIdProductRequest, ProductResponse>
{
    private readonly IProductService _productService;

    public GetByShortIdProductHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductResponse> Handle(GetByShortIdProductRequest request, CancellationToken cancellationToken)
    {
        return await _productService.GetByShortIdAsync(request.ShortId);       
    }
}

