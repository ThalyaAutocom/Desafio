using Desafio.Domain;

namespace Desafio.Application;
public interface IProductService
{
    Task<CreateProductResponse> InsertAsync(CreateProductRequest productRequest);
    Task<bool> UpdateAsync(UpdateProductRequest productRequest);
    Task<bool> RemoveAsync(string shortId);
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<bool> ExistingBarCodeAsync(string barCode);
    Task<bool> ExistingBarCodeAsync(UpdateProductRequest productRequest);
    Task<bool> UnitAlreadyExistsAsync(string acronym);
    Task<bool> UnitAlreadyExistsAsync(UpdateProductRequest productRequest);
    Task<ProductResponse> GetByShortIdAsync(string shortId);
}

