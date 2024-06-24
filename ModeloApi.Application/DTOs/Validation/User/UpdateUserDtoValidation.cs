
using FluentValidation;
using ModeloApi.Application.DTOs.User;

namespace ModeloApi.Application.DTOs.Validation.User;
public class UpdateUserDtoValidation : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Invalid id! Id must be greater than 0!");

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required!");

        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("Invalid name! Minumum 3 characteres!");

        RuleFor(x => x.Group)
           .NotEmpty()
           .NotNull()
           .WithMessage("Group is required!");

        RuleFor(x => x.Group)
            .MinimumLength(3)
            .WithMessage("Invalid group! Minumum 3 characteres!");
    }
}
