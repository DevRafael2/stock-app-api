using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Application.Dtos.Entities.Stock;
using StockApp.Core.Application.UseCases.Entities.Product.Commands;
using StockApp.Core.Application.UseCases.Entities.Product.Queries;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Web.Api.Controllers.V1;

/// <summary>
/// Controlador de productos
/// </summary>
/// <param name="sender">Sender de MediatR</param>
[ApiController, Route("api/v1/products"), Authorize]
public class ProductController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Endpoint para obtener productos
    /// </summary>
    /// <param name="productQueryParams">Query params de productos</param>
    /// <returns>Retorna una respuesta con información de los productos paginados</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ResponseApiDataPaginate<OutProduct>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync([FromQuery] GetProductsRequest productQueryParams)
    {
        var result = await sender.Send(productQueryParams);
        return StatusCode((int)result.StatusResponse, result);
    }
}