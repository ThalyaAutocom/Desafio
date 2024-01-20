using MediatR;

namespace Desafio.Application;
public class GetByIdProductHandler : IRequestHandler<GetByIdProductRequest, ProductResponse>
{
    private readonly IProductService _productService;

    public GetByIdProductHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductResponse> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
    {
        return await _productService.GetByIdAsync(request.Id);       
    }
}

