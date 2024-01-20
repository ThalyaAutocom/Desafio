using MediatR;

namespace Desafio.Application;

public class CreateProductHandle : IRequestHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IProductService _productService;

    public CreateProductHandle(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.InsertAsync(request);

        return result;
    }
}
