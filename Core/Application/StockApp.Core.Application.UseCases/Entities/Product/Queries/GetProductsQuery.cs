using System.Linq.Expressions;
using MapsterMapper;
using StockApp.Core.Application.Dtos.Entities.Stock;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.UseCases.Base.Queries;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Core.Application.UseCases.Entities.Product.Queries;

using Domain.Entities.Stock;

/// <summary>
/// Request QueryParams para productos
/// </summary>
public class GetProductsRequest : QueryParams<Product, OutProduct>
{
    public override Expression<Func<Product, bool>> GetWhereExpression() => e => true;

    public override Expression<Func<Product, Product>> GetSelectExpression() => e => e;
}

/// <summary>
/// Query para obtener productos
/// </summary>
/// <param name="unitOfWork">Unidad de trabajo</param>
/// <param name="mapper">Mapster</param>
public class GetProductsQuery(IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseGetQuery<Product, int, GetProductsRequest, OutProduct>(unitOfWork, mapper);