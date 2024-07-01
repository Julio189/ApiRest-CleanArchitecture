
using FluentValidation;

namespace ModeloApi.Infra.Data.Authentication.AuthDtos.Validation;
public class RoleDtoValidation : AbstractValidator<RoleDto>
{
    public RoleDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Role name is required!");

        RuleFor(x => x.Name)
            .MinimumLength(2)
            .WithMessage("Invalid name! Minimum 2 characteres!");
    }
}
