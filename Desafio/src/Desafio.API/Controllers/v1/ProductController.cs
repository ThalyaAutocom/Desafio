using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class ProductController : DesafioControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService, IError error) : base(error)
    {
        _productService = productService;
    }

    #region Get
    /// <summary>
    /// Select Product By Id
    /// </summary>
    /// <remarks>Searchs an especific product on database, using its Id</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id")]
    public async Task<ActionResult<ProductResponse>> GetProductAsync(Guid id)
    {
        ProductResponse result = await _productService.GetByIdAsync(id);

        return CustomResponse(result);
    }

    /// <summary>
    /// Select all Products
    /// </summary>
    /// <remarks>Searchs all products registered in database</remarks>
    /// <returns></returns>
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllAsync();
        if (!result.Any()) return CustomResponseList(result, "No Products were found.");

        return CustomResponseList(result);
    }

    /// <summary>
    /// Select all Sellable Products 
    /// </summary>
    /// <remarks>Searchs all products that are available for sale</remarks>
    /// <returns></returns>
    [HttpGet("get-all-sellable")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllSalabeProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllSellableAsync();
        if (!result.Any()) return CustomResponseList(result, "No Products were found.");

        return CustomResponseList(result);
    }
    /// <summary>
    /// Select Product by Short Id
    /// </summary>
    /// <remarks>Searchs an especific product on database, using its Short Id</remarks>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<ProductResponse>> GetProductByShortIdAsync(string shortId)
    {
        ProductResponse result = await _productService.GetByShortIdAsync(shortId);

        return CustomResponse(result);
    }
    #endregion

    #region Post
    /// <summary>
    /// Insert Product
    /// </summary>
    /// <remarks>Inserts one product on database</remarks>
    /// <param name="productRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-product")]
    public async Task<ActionResult<ProductResponse>> InsertProductAsync(InsertProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.InsertAsync(productRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Put
    /// <summary>
    /// Update Product
    /// </summary>
    /// <remarks>Updates a product information</remarks>
    /// <param name="productRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-product-information")]
    public async Task<ActionResult<ProductResponse>> UpdateProductAsync(ProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.UpdateAsync(productRequest);

        return CustomResponse(result);
    }

    /// <summary>
    /// Update Enable Property
    /// </summary>
    /// <remarks>Updates only the Enable property</remarks>
    /// <param name="productRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-enabled-property")]
    public async Task<ActionResult<bool>> UpdateProductEnabled(EnabledProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.UpdateEnableProductAsync(productRequest);

        return CustomResponse(result);
    }

    /// <summary>
    /// Update Sellable property
    /// </summary>
    /// <remarks>Updates only the Sellable property. This property indicates that the product is able to be selled</remarks>
    /// <param name="productRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-sellable-property")]
    public async Task<ActionResult<bool>> UpdateProductSellable(SellableProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.UpdateSellableProductAsync(productRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Delete product
    /// </summary>
    /// <remarks>Removes an especifc product prom the database</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-product")]
    public async Task<ActionResult<ProductResponse>> RemoveProductAsync(Guid id)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.RemoveAsync(id);

        return CustomResponse(result);
    }
    #endregion






    
}
