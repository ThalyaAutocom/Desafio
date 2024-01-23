using MediatR;

namespace Desafio.Application;
public class GetProductHandler : IRequestHandler<GetProductRequest, GetProductResponse>
{
    private readonly IProductService _productService;

    public GetProductHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<GetProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.GetAllAsync();

        if(request.Sellable is not null)
        {
            result = result.Where(x => x.Sellable == request.Sellable).ToList();
        }
        if (request.Enable is not null)
        {
            result = result.Where(x => x.Enable == request.Enable).ToList();
        }

        return new GetProductResponse
        {
            ProductResponses = result
        };
    }
}

