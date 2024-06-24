
using FluentValidation;
using ModeloApi.Application.DTOs.Person;

namespace ModeloApi.Application.DTOs.Validation.Person;
public class CreatePersonDtoValidation : AbstractValidator<CreatePersonDto>
{
    public CreatePersonDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required!");

        RuleFor(x => x.Name)
            .MaximumLength(150)
            .WithMessage("Invalid Name! Maximum 150 characteres!");

        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("Invalid Name! Minimum 3 characteres!");

        RuleFor(x => x.Document)
            .NotEmpty()
            .NotNull()
            .WithMessage("Document is required!");

        RuleFor(x => x.Document)
            .MinimumLength(11)
            .MaximumLength(11)
            .WithMessage("Invalid Document! Document must have 11 characteres!");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .NotNull()
            .WithMessage("Phone is required!");

        RuleFor(x => x.Phone)
            .MinimumLength(11)
            .WithMessage("Invalid Phone! Minimum 11 characteres!");

        RuleFor(x => x.Phone)
            .MaximumLength(13)
            .WithMessage("Invalid Phone! Maximum 13 characteres!");
    }
}
