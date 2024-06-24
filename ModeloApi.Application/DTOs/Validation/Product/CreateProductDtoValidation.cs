
using FluentValidation;
using ModeloApi.Application.DTOs.Product;

namespace ModeloApi.Application.DTOs.Validation.Product;
public class CreateProductDtoValidation : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required!");

        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("Invalid Name! Minimum 3 characteres!");

        RuleFor(x => x.CodErp)
            .NotEmpty()
            .NotNull()
            .WithMessage("CodErp is required!");

        RuleFor(x => x.CodErp)
            .MinimumLength(3)
            .WithMessage("Invalid CodErp! Minimum 3 characteres!");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Invalid Price! Price must be greater than 0!");
    }
}
