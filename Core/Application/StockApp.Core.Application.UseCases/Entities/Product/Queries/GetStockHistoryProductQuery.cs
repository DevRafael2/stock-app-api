using System.Linq.Expressions;
using MapsterMapper;
using MediatR;
using StockApp.Core.Application.Dtos.Entities.Stock;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.UseCases.Base.Queries;
using StockApp.Core.Domain.Entities.Stock;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Core.Application.UseCases.Entities.Product.Queries;

/// <summary>
/// Request para obtener historial paginado de un producto
/// </summary>
public class GetStockHistoryProductRequest : QueryParams<StockHistoryProduct, OutStockHistoryProduct>
{
    /// <summary>
    /// Id del producto
    /// </summary>
    public int? ProductId { get; set; } = null;
    public override Expression<Func<StockHistoryProduct, bool>> GetWhereExpression() => e => 
        ProductId == null || e.ProductId == ProductId;

    public override Expression<Func<StockHistoryProduct, StockHistoryProduct>> GetSelectExpression() => e =>
        new()
        {
            Amount = e.Amount,
            Product = new() { Name = e.Product!.Name },
            CreatedAt = e.CreatedAt
        };
}

/// <summary>
/// Query para obtener historial de movimientos de productos
/// </summary>
/// <param name="unitOfWork">Unidad de trabajo</param>
/// <param name="mapper">Mapster</param>
public class GetStockHistoryProductQuery(IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseGetQuery<StockHistoryProduct, int, GetStockHistoryProductRequest, OutStockHistoryProduct>(unitOfWork, mapper);