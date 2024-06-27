
using FluentValidation;
using ModeloApi.Application.DTOs.AuthenticationDtos;

namespace ModeloApi.Application.DTOs.Validation.AuthenticationDtos;
public class IdentityLoginDtoValidation : AbstractValidator<IdentityLoginDto>
{
    public IdentityLoginDtoValidation()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage("User name is required!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password is required!");
    }
}
