
using FluentValidation;
using ModeloApi.Application.DTOs.Purchase;

namespace ModeloApi.Application.DTOs.Validation.Purchase;
public class UpdatePurchaseDtoValidation : AbstractValidator<UpdatePurchaseDto>
{
    public UpdatePurchaseDtoValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Invalid Id! Id must be greater than 0!");

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
