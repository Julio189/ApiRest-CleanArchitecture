
using FluentValidation;
using ModeloApi.Application.DTOs.User;

namespace ModeloApi.Application.DTOs.Validation.User;
public class CreateUserDtoValidation : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required!");

        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("Invalid name! Minumum 3 characteres!");

        RuleFor(x => x.Password)
           .NotEmpty()
           .NotNull()
           .WithMessage("Password is required!");

        RuleFor(x => x.Password)
            .MinimumLength(3)
            .WithMessage("Invalid password! Minumum 3 characteres!");

        RuleFor(x => x.Group)
           .NotEmpty()
           .NotNull()
           .WithMessage("Group is required!");

        RuleFor(x => x.Group)
            .MinimumLength(3)
            .WithMessage("Invalid group! Minumum 3 characteres!");
    }
}
