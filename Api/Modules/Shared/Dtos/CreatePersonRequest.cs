using FluentValidation;

namespace Api.Modules.Shared.Dtos;

public record CreatePersonRequest(
    string Dni,
    string FirstName,
    string LastName,
    string? BirthDate,
    string? Phone,
    string? Email,
    string? Address
);


public class CreatePersonRequestValidator : AbstractValidator<CreatePersonRequest>
{
    public CreatePersonRequestValidator()
    {
        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("Dni is required")
            .MinimumLength(10)
            .MaximumLength(15).WithMessage("Dni must be 15 characters or less");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .MaximumLength(150);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .MaximumLength(150);

        RuleFor(x => x.Phone)
            .MaximumLength(15)
            .When(x => !string.IsNullOrEmpty(x.Phone));
    }
}