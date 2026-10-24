using Api.Modules.Shared.Dtos;
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
            .MaximumLength(150);

        RuleFor(x => x.Doctor.Speciality)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Doctor.Person)
            .SetValidator(new UpdatePersonRequestValidator());
    }
} 