
using FluentValidation;

namespace ModeloApi.Infra.Data.Authentication.AuthDtos.Validation;
public class LoginDtoValidation : AbstractValidator<LoginDto>
{
    public LoginDtoValidation()
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
