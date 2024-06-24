
using FluentValidation;
using ModeloApi.Application.DTOs.User;

namespace ModeloApi.Application.DTOs.Validation.User;
public class UpdateUserPasswordDtoValidation : AbstractValidator<UpdateUserPasswordDto>
{
    public UpdateUserPasswordDtoValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Invalid id! Id must be greater than 0!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password is required!");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .NotNull()
            .WithMessage("Confirm password is required!");
    }
}
