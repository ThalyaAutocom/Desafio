using Desafio.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class ProductController : DesafioControllerBase
{
    public ProductController() 
    {

    }

    #region Get
    /// <summary>
    /// Retornar todos os produtos
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all")]
    public async Task<ActionResult<GetProductResponse>> GetAllProductsAsync(ISender mediator,
        bool? sellable,
        bool? enable,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetProductRequest(sellable, enable), cancellationToken);
    }

    /// <summary>
    /// Retornar produtos por Short Id
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="shortId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<ProductResponse>> GetProductByShortIdAsync(ISender mediator,
        string shortId,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetByShortIdProductRequest(shortId), cancellationToken);
    }
    #endregion

    #region Post
    /// <summary>
    /// Cadastrar produto
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="productRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("create-product")]
    public async Task<ActionResult<CreateProductResponse>> CreateProductAsync(ISender mediator,
        CreateProductRequest productRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(productRequest, cancellationToken);
    }
    #endregion

    #region Put
    /// <summary>
    /// Atualizar produto
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="productRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-product")]
    public async Task<ActionResult<bool>> UpdateProductAsync(ISender mediator,
        UpdateProductRequest productRequest, 
        CancellationToken cancellationToken)
    {
        return await mediator.Send(productRequest, cancellationToken);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir Produto
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="productRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-product")]
    public async Task<ActionResult<bool>> RemoveProductAsync(ISender mediator,
        DeleteProductRequest productRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(productRequest, cancellationToken);
    }
    #endregion
}
