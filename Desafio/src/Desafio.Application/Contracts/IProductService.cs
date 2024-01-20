using Desafio.Domain;

namespace Desafio.Application;
public interface IProductService
{
    Task<CreateProductResponse> InsertAsync(CreateProductRequest productRequest);
    Task<bool> UpdateAsync(UpdateProductRequest productRequest);
    Task<bool> RemoveAsync(Guid id);
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<bool> ExistingBarCodeAsync(string barCode);
    Task<bool> UnitAlreadyExistsAsync(string acronym);
    Task<ProductResponse> GetByShortIdAsync(string shortId);
}

