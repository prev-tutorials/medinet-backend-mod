using Api.Modules.Shared.Application.Dtos.Persons;
using FluentValidation;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.UpdateDoctor;

public record UpdateDoctorRequest(
    string? Speciality,
    string? NRegister,
    bool?   Active,
    UpdatePersonRequest Person
);

public record UpdateDoctorCommand(
    Guid Id,
    UpdateDoctorRequest Doctor
):IRequest<Guid>;


public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorValidator()
    {
        RuleFor(x => x.Doctor.NRegister)
            .NotEmpty().WithMessage("NRegister is required")
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.Doctor.NRegister));

        RuleFor(x => x.Doctor.Speciality)
            .NotEmpty()
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.Doctor.Speciality));

        RuleFor(x => x.Doctor.Person)
            .SetValidator(new UpdatePersonRequestValidator())
            .When(x => x.Doctor.Person != null);
    }
} 