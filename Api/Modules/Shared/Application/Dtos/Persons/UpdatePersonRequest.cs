using Api.Modules.Shared.Application.Helpers;
using FluentValidation;

namespace Api.Modules.Shared.Application.Dtos.Persons;

public record UpdatePersonRequest(
    string? Dni,
    string? FirstName,
    string? LastName,
    string? BirthDate,
    string? Phone,
    string? Email,
    string? Address
);


public class UpdatePersonRequestValidator : AbstractValidator<UpdatePersonRequest>
{
    public UpdatePersonRequestValidator()
    {
        RuleFor(x => x.Dni)
            .MinimumLength(10).WithMessage("Dni must be at least 10 characters")
            .MaximumLength(15).WithMessage("Dni must be 15 characters or less")
            .When(x => !string.IsNullOrWhiteSpace(x.Dni));

        RuleFor(x => x.FirstName)
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.Phone)
            .MaximumLength(15)
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.BirthDate)
            .Must(DateHelper.IsValidStringDate)
            .WithMessage("BirthDate must be in format yyyy-MM-dd")
            .When(x => !string.IsNullOrWhiteSpace(x.BirthDate));
    }
}