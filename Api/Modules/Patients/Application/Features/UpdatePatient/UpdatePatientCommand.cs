using Api.Modules.Shared.Dtos;
using FluentValidation;
using MediatR;

namespace Api.Modules.Patients.Application.Features.UpdatePatient;


public record UpdatePatientRequest(
    string? Nhc,
    UpdatePersonRequest Person
);

public record UpdatePatientCommand(
    Guid Id, 
    UpdatePatientRequest Patient
):IRequest<Guid>;


public class UpdatePatientValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required");

        RuleFor(x => x.Patient)
            .NotNull().WithMessage("Patient data is required");

        RuleFor(x => x.Patient.Nhc)
            .MaximumLength(150)
            .Must(nhc => !string.IsNullOrWhiteSpace(nhc)).WithMessage("Nhc cannot be empty or whitespace")
            .When(x => x.Patient.Nhc != null);

        RuleFor(x => x.Patient.Person)
            .SetValidator(new UpdatePersonRequestValidator())
            .When(x => x.Patient.Person != null);
    }
}