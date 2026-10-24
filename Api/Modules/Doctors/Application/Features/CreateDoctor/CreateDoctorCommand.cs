using Api.Modules.Shared.Dtos;
using FluentValidation;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.CreateDoctor;

public record CreateDoctorRequest(
    string NRegister,
    string Speciality,
    CreatePersonRequest Person
);


public record CreateDoctorCommand(
    Guid Id,
    CreateDoctorRequest Doctor
):IRequest<Guid>;


public class CreateDoctorValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.Doctor.NRegister)
            .NotEmpty().WithMessage("NRegister is required")
            .MaximumLength(150);

        RuleFor(x => x.Doctor.Speciality)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Doctor.Person)
            .SetValidator(new CreatePersonRequestValidator());
    }
} 