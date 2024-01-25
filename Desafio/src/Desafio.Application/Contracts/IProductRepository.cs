using Desafio.Domain;

namespace Desafio.Application;

public interface IProductRepository
{
    Task InsertAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task RemoveAsync(string shortId);
    Task<Product> GetByBarCodeAsync(string barCode);
    Task<Product> GetByBarCodeAsync(UpdateProductRequest request);
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetAllSellableAsync();
    Task<int> SaveChangesAsync();
    Task<bool> UnitAlreadyExistsAsync(string acronym);
    Task<Product> GetByShortIdAsync(string shortId);
}
