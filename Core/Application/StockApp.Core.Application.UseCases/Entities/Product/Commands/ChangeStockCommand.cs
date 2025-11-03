using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using StockApp.Core.Application.Dtos.Entities.Stock;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Core.Application.UseCases.Entities.Product.Commands;

using Domain.Entities.Stock;

/// <summary>
/// Request para realizar cambios en el stock 
/// </summary>
/// <param name="Entity">Stock a registrar</param>
public record ChangeStockRequest(InStockHistoryProduct? Entity) : IRequest<ResponseApi>;

/// <summary>
/// Comando para actualizar el stock de un producto
/// </summary>
/// <param name="validator">Servicio de validación de fluent validation</param>
/// <param name="httpContextAccesor">Servicio para acceder al httpcontext</param>
/// <param name="mapper">Mapster</param>
/// <param name="unitOfWork">Unidad de trabajo</param>
public class ChangeStockCommand(
    IValidator<ChangeStockRequest> validator,
    IHttpContextAccessor httpContextAccesor,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<ChangeStockRequest, ResponseApi>
{

    public async Task<ResponseApi> Handle(ChangeStockRequest request, CancellationToken cancellationToken)
    {
        var resultValidation = await validator.ValidateAsync(request, cancellationToken);
        if(!resultValidation.IsValid) return new ResponseApi()
            .BadRequest("La información contiene errores")
            .AddNewErrors(resultValidation.Errors.Select(e => e.ErrorMessage));
        
        var userId = httpContextAccesor.HttpContext.User?.Identity!.Name;
        
        var updateProduct = await unitOfWork.GetRepository<Product, int>()
            .GetFirstAsync(request.Entity!.ProductId);

        if (updateProduct is null)
            return new ResponseApi().NotFound("No se ha encontrado el producto solicitado");
        
        updateProduct.Amount += request.Entity.Amount;
        
        var repository = unitOfWork.GetRepository<StockHistoryProduct, int>();
        var createEntity = mapper.Map<StockHistoryProduct>(request.Entity);
        createEntity.UserId = Guid.Parse(userId!);
        var entityCreate = await repository.CreateAsync(createEntity);
        await unitOfWork.CompleteAsync();
        
        var entityOutCreate = mapper.Map<OutSampleStockHistoryProduct>(entityCreate);
        return new ResponseApiData<OutSampleStockHistoryProduct>().Success(entityOutCreate, "Movimiento registrado correctamente");
    }
}