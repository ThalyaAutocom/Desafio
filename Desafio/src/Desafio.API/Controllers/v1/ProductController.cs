using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class ProductController : DesafioControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService) 
    {
        _productService = productService;
    }

    #region Get
    /// <summary>
    /// Retornar produto por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-id")]
    public async Task<ActionResult<ProductResponse>> GetProductAsync(Guid id)
    {
        ProductResponse result = await _productService.GetByIdAsync(id);

        return null;
    }

    /// <summary>
    /// Retornar todos os produtos
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllAsync();

        return null;
    }

    /// <summary>
    /// Retornar todos os produtos vendáveis 
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-sellable")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllSalabeProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllSellableAsync();

        return null;
    }
    /// <summary>
    /// Retornar produtos por Short Id
    /// </summary>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<ProductResponse>> GetProductByShortIdAsync(string shortId)
    {
        ProductResponse result = await _productService.GetByShortIdAsync(shortId);

        return null;
    }
    #endregion

    #region Post
    /// <summary>
    /// Cadastrar produto
    /// </summary>
    /// <param name="productRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-product")]
    public async Task<ActionResult<ProductResponse>> InsertProductAsync(InsertProductRequest productRequest)
    {
        ProductResponse result = await _productService.InsertAsync(productRequest);

        return null;
    }
    #endregion

    #region Put
    /// <summary>
    /// Atualizar produto
    /// </summary>
    /// <param name="productRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-product-information")]
    public async Task<ActionResult<ProductResponse>> UpdateProductAsync(UpdateProductRequest productRequest)
    {
        ProductResponse result = await _productService.UpdateAsync(productRequest);

        return null;
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir Produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-product")]
    public async Task<ActionResult<ProductResponse>> RemoveProductAsync(Guid id)
    {
        ProductResponse result = await _productService.RemoveAsync(id);

return null;    }
    #endregion
}
