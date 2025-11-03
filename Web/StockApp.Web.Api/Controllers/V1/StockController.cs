using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Application.Dtos.Entities.Stock;
using StockApp.Core.Application.UseCases.Entities.Product.Commands;
using StockApp.Core.Application.UseCases.Entities.Product.Queries;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Web.Api.Controllers.V1;

/// <summary>
/// Controlador para historial de movimientos de productos
/// </summary>
/// <param name="sender">Sender de mediatR</param>
[ApiController, Route("api/v1/products/stock"), Authorize]
public class StockController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Endpoint para obtener historial de movimientos de productos
    /// </summary>
    /// <param name="stockHistoryProductRequest">Query params</param>
    /// <returns>Retorna respuesta paginada de historial de productos</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ResponseApiDataPaginate<OutStockHistoryProduct>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStockHistoryProducts(
        [FromQuery]GetStockHistoryProductRequest stockHistoryProductRequest)
    {
        var result = await sender.Send(stockHistoryProductRequest);
        return StatusCode((int)result.StatusResponse, result);
    }
    
    /// <summary>
    /// Endpoint para cambiar el stock de un producto
    /// </summary>
    /// <returns>Retorna una respuesta relacionada al éxito de la operación</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseApiData<OutSampleStockHistoryProduct>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeStock([FromBody]InStockHistoryProduct stockProduct)
    {
        var result = await sender.Send(new ChangeStockRequest(stockProduct));
        return StatusCode((int)result.StatusResponse, result);
    }
}