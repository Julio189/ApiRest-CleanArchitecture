
using FluentValidation;
using ModeloApi.Application.DTOs.AuthenticationDtos;

namespace ModeloApi.Application.DTOs.Validation.AuthenticationDtos;
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
