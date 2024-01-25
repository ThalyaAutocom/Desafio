using Desafio.Domain;
using Desafio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _appDbContext.Products.AsNoTracking().ToListAsync();
    }
    public async Task<List<Product>> GetAllSellableAsync()
    {
        return await _appDbContext.Products.AsNoTracking().Where(x => x.Sellable).ToListAsync();
    }

    public async Task<Product> GetByBarCodeAsync(string barCode)
    {
        return await _appDbContext.Products.AsNoTracking().SingleOrDefaultAsync(x => x.BarCode == barCode);
    }

    public async Task<Product> GetByBarCodeAsync(UpdateProductRequest request)
    {
        return await _appDbContext.Products.AsNoTracking().SingleOrDefaultAsync(x => x.BarCode == request.BarCode && x.ShortId != request.ShortId);
    }

    public async Task InsertAsync(Product product)
    {
        try
        {
            await _appDbContext.Products.AddAsync(product);
            await SaveChangesAsync();
        }
        catch (CustomException)
        {
            throw new CustomException("Error while inserting product");
        }
    }

    public async Task RemoveAsync(string shortId)
    {
        try
        {
            Product product = await GetByShortIdAsync(shortId);
            if (product == null)
            {
                throw new CustomException("The product was not found.");
            }
            _appDbContext.Products.Remove(product);
            await SaveChangesAsync();
        }
        catch (CustomException)
        {
            throw;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _appDbContext.SaveChangesAsync();
        }
        catch (CustomException)
        {
            throw new CustomException("Error while saving product");
        }
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        try
        {
            _appDbContext.Update(product);
            await SaveChangesAsync();
            return product;
        }
        catch (CustomException)
        {
            throw new CustomException("Error while updating product");
        }
    }

    public async Task<Product> GetByShortIdAsync(string shortId)
    {
        return await _appDbContext.Products.AsNoTracking().SingleOrDefaultAsync(x => x.ShortId == shortId);
    }

    public async Task<bool> UnitAlreadyExistsAsync(string acronym)
    {
        return await _appDbContext.Units.AsNoTracking().SingleOrDefaultAsync(x => x.Acronym == acronym) != null;
    }
}
