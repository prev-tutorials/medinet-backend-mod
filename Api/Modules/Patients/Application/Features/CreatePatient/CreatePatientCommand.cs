using Api.Modules.Shared.Dtos;
using FluentValidation;
using MediatR;

namespace Api.Modules.Patients.Application.Features.CreatePatient;

public record CreatePatientRequest(
    string Nhc,
    CreatePersonRequest Person
);


public record CreatePatientCommand(
    CreatePatientRequest Patient
):IRequest<Guid>;


public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientValidator()
    {
        RuleFor(x => x.Patient.Nhc)
            .NotEmpty().WithMessage("Nhc is required")
            .MaximumLength(150);

        RuleFor(x => x.Patient.Person)
            .NotNull().WithMessage("Person data is required")
            .SetValidator(new CreatePersonRequestValidator());
    }
} 