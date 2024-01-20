using MediatR;

namespace Desafio.Application;

public class DeleteProductHandle : IRequestHandler<DeleteProductRequest, bool>
{
    private readonly IProductService _productService;

    public DeleteProductHandle(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<bool> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        return await _productService.RemoveAsync(request.Id);
    }
}
