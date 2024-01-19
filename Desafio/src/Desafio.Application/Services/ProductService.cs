﻿using AutoMapper;
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
            throw new Exception("No products were found.");
        }

        return result;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllSellableAsync()
    {
        var result = _mapper.Map<IEnumerable<ProductResponse>>(await _productRepository.GetAllSellableAsync());

        if (result == null)
        {
            throw new Exception("No products were found.");
        }

        return result;
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new Exception("Product was not found.");
        }

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> GetByShortIdAsync(string shortId)
    {
        var product = await _productRepository.GetByShortIdAsync(shortId);

        if (product == null)
        {
            throw new Exception("Product was not found.");
        }

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> InsertAsync(InsertProductRequest productRequest)
    {
        var product = _mapper.Map<Product>(productRequest);

        await _productRepository.InsertAsync(product);
        var newProduct = _mapper.Map<ProductResponse>(product);
        return newProduct;
    }

    public async Task<ProductResponse> RemoveAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new Exception("Product was not found.");
        }
        await _productRepository.RemoveAsync(id);

        return null;
    }

    public async Task<ProductResponse> UpdateAsync(UpdateProductRequest productRequest)
    {
        var existingProduct = await _productRepository.GetByIdAsync(productRequest.Id);

        if (existingProduct == null)
        {
            throw new Exception("The product was not found.");
        }

        _mapper.Map(productRequest, existingProduct);

        await _productRepository.UpdateAsync(existingProduct);

        var productResponse = _mapper.Map<ProductResponse>(existingProduct);

        return productResponse;
    }
    #endregion

    #region Validations Methods
    public async Task<bool> ExistingBarCodeAsync(string barCode)
    {
        return await _productRepository.GetByBarCodeAsync(barCode) != null;
    }

    public async Task<bool> UnitAlreadyExistsAsync(string acronym)
    {
        return await _productRepository.UnitAlreadyExistsAsync(acronym);
    }
    #endregion
}
