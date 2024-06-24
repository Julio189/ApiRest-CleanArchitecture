
using FluentValidation;
using ModeloApi.Application.DTOs.Purchase;

namespace ModeloApi.Application.DTOs.Validation.Purchase;
public class CreatePurchaseDtoValidation : AbstractValidator<CreatePurchaseDto>
{
    public CreatePurchaseDtoValidation()
    {
        RuleFor(x => x.CodErp)
            .NotEmpty()
            .NotNull()
            .WithMessage("CodErp is required!");

        RuleFor(x => x.Document)
            .NotEmpty()
            .NotNull()
            .WithMessage("Document is required!");
    }
}
