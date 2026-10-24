using Api.Modules.Shared.Dtos;
using FluentValidation;
using MediatR;

namespace Api.Modules.Patients.Application.Features.CreatePatient;

public record CreatePatientRequest(
    string Nhc
);


public record CreatePatientCommand(
    Guid Id,
    CreatePatientRequest Patient,
    CreatePersonRequest Person
):IRequest<Guid>;


public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientValidator()
    {
        RuleFor(x => x.Patient.Nhc)
            .NotEmpty().WithMessage("Nhc is required")
            .MaximumLength(150);

        RuleFor(x => x.Person)
            .SetValidator(new CreatePersonRequestValidator());
    }
} 