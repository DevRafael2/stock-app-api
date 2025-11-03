using MapsterMapper;
using MediatR;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Core.Application.UseCases.Base.Queries;

/// <summary>
/// Query base para obtener entidades paginadas
/// </summary>
/// <typeparam name="TEntity">Tipo de la entidad</typeparam>
/// <typeparam name="TId">Tipo del id de la entidad</typeparam>
/// <typeparam name="TQueryParams">Tipo del queryParams</typeparam>
/// <typeparam name="TOutDto">Tipo del dto de salida</typeparam>
/// <param name="unitOfWork">Unidad de trabajo</param>
/// <param name="mapper">Mapster</param>
public class BaseGetQuery<TEntity, TId, TQueryParams, TOutDto>(
    IUnitOfWork unitOfWork,
    IMapper mapper) : 
    IRequestHandler<TQueryParams, ResponseApiDataPaginate<TOutDto>> 
    where TEntity : EntityRoot<TId>, new()
    where TQueryParams : QueryParams<TEntity, TOutDto>
{
    /// <summary>
    /// Handler para obtener registros paginados
    /// </summary>
    /// <param name="request">Request para filtros y selección de propiedades</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Retorna una respuesta con datos paginados</returns>
    public async Task<ResponseApiDataPaginate<TOutDto>> Handle(TQueryParams request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.GetRepository<TEntity, TId>()
            .GetAsync(request.GetWhereExpression(), request.GetSelectExpression(), 
                request.CurrentPage, request.PageSize);

        var outData = mapper.Map<List<TOutDto>>(data.Data ?? []);
        return new ResponseApiDataPaginate<TOutDto>()
        {
            CountData = data.CountData,
            CountPages = data.CountPages,
            ActualPage = data.ActualPage,
            Data = outData,
            Message = "Se obtienen los datos correctamente",
            StatusResponse = StatusResponse.Ok,
        };
    }
}