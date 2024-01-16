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
    /// Retornar produto por Id
    /// </summary>
    /// <remarks>Retorna um produto específico, pesquisando por seu Id</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-id")]
    public async Task<ActionResult<ProductResponse>> GetProductAsync(Guid id)
    {
        ProductResponse result = await _productService.GetByIdAsync(id);

        return CustomResponse(result);
    }

    /// <summary>
    /// Retornar todos os produtos
    /// </summary>
    /// <remarks>Retorna todos os produtos cadastrados</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllAsync();
        if (!result.Any()) return CustomResponseList(result, "No Products were found.");

        return CustomResponseList(result);
    }

    /// <summary>
    /// Retornar todos os produtos vendáveis 
    /// </summary>
    /// <remarks>Retorna todos os produtos que estão com a propriedade "sellable" setadas como "true"</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-sellable")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllSalabeProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllSellableAsync();
        if (!result.Any()) return CustomResponseList(result, "No Products were found.");

        return CustomResponseList(result);
    }
    /// <summary>
    /// Retornar produtos por Short Id
    /// </summary>
    /// <remarks>Retorna um produto específico, pesquisando por seu Short Id</remarks>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<ProductResponse>> GetProductByShortIdAsync(string shortId)
    {
        ProductResponse result = await _productService.GetByShortIdAsync(shortId);

        return CustomResponse(result);
    }
    #endregion

    #region Post
    /// <summary>
    /// Cadastrar produto
    /// </summary>
    /// <remarks>Cadastra um produto</remarks>
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
    /// Atualizar produto
    /// </summary>
    /// <remarks>Atualiza informações de um produto</remarks>
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
    /// Atualiza propriedade "Enable" (Ativo)
    /// </summary>
    /// <remarks>Atualiza somente a propriedade "enable", indicando se o produto está ativo ou não</remarks>
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
    /// Atualiza propriedade "Sellable" (Vendável)
    /// </summary>
    /// <remarks>Atualiza somente a propriedade "sellable", indicando se o produto está disponível ou não para venda</remarks>
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
    /// Excluir Produto
    /// </summary>
    /// <remarks>Exclui um cadastro de produto</remarks>
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
