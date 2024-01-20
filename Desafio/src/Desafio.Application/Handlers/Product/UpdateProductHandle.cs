using MediatR;

namespace Desafio.Application;

public class UpdateProductHandle : IRequestHandler<UpdateProductRequest, bool>
{
    private readonly IProductService _productService;

    public UpdateProductHandle(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<bool> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateAsync(request);

        return result;
    }
}
