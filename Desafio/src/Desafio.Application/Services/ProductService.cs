using AutoMapper;
using Desafio.Domain;

namespace Desafio.Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper) 
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    #region Controller Methods
    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var result = _mapper.Map<IEnumerable<ProductResponse>>(await _productRepository.GetAllAsync());

        if (result == null)
        {
            throw new CustomException("No products were found.");
        }

        return result;
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new CustomException("The product was not found.");
        }

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> GetByShortIdAsync(string shortId)
    {
        var product = await _productRepository.GetByShortIdAsync(shortId);

        if (product == null)
        {
            throw new CustomException("The product was not found.");
        }

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<CreateProductResponse> InsertAsync(CreateProductRequest productRequest)
    {
        var product = _mapper.Map<Product>(productRequest);

        await _productRepository.InsertAsync(product);
        var newProduct = _mapper.Map<CreateProductResponse>(product);
        return newProduct;
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new CustomException("The product was not found.");
        }
        await _productRepository.RemoveAsync(id);

        return true;
    }

    public async Task<bool> UpdateAsync(UpdateProductRequest productRequest)
    {
        var existingProduct = await _productRepository.GetByIdAsync(productRequest.Id);

        if (existingProduct == null)
        {
            throw new CustomException("The product was not found.");
        }

        _mapper.Map(productRequest, existingProduct);

        await _productRepository.UpdateAsync(existingProduct);

        return true;
    }
    #endregion

    #region Validations Methods
    public async Task<bool> ExistingBarCodeAsync(string barCode)
    {
        if (string.IsNullOrWhiteSpace(barCode)) return false;
        return await _productRepository.GetByBarCodeAsync(barCode) != null;
    }
    public async Task<bool> ExistingBarCodeAsync(UpdateProductRequest productRequest)
    {
        if (string.IsNullOrWhiteSpace(productRequest.BarCode)) return false;
        return await _productRepository.GetByBarCodeAsync(productRequest.BarCode) != null;
    }

    public async Task<bool> UnitAlreadyExistsAsync(string acronym)
    {
        if (string.IsNullOrWhiteSpace(acronym)) return false;
        return await _productRepository.UnitAlreadyExistsAsync(acronym);
    }
    public async Task<bool> UnitAlreadyExistsAsync(UpdateProductRequest productRequest)
    {
        if (string.IsNullOrWhiteSpace(productRequest.Acronym)) return false;
        return await _productRepository.UnitAlreadyExistsAsync(productRequest.Acronym);
    }
    #endregion
}
