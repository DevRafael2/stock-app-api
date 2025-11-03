using FluentValidation;
using StockApp.Core.Application.UseCases.Entities.Product.Commands;

namespace StockApp.Core.Application.Validators.Commands;

/// <summary>
/// Validador para el ChangeStockRequest
/// </summary>
public class ChangeStockCommandValidator : AbstractValidator<ChangeStockRequest>
{
    /// <summary>
    /// Constructor de reglas para el validador
    /// </summary>
    public ChangeStockCommandValidator()
    {
        RuleFor(e => e.Entity)
            .NotNull().WithMessage("Debe proporcionar información");

        RuleFor(e => e.Entity!.Amount)
            .NotEqual(0).WithMessage("El valor no puede ser 0");
        
        RuleFor(e => e.Entity!.ProductId)
            .NotEqual(0).WithMessage("Debe seleccionar un producto");
    }
}