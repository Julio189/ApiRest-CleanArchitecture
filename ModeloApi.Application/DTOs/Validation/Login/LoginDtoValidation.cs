
using FluentValidation;
using ModeloApi.Application.DTOs.Login;

namespace ModeloApi.Application.DTOs.Validation.Login;
public class LoginDtoValidation : AbstractValidator<LoginDto>
{
    public LoginDtoValidation()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .NotNull()
           .WithMessage("Name is required!");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password is required!");
    }
}
