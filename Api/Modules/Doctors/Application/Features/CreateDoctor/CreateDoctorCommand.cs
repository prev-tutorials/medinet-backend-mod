using Api.Modules.Shared.Application.Dtos.Persons;
using FluentValidation;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.CreateDoctor;

public record CreateDoctorRequest(
    string NRegister,
    string Speciality,
    CreatePersonRequest Person
);


public record CreateDoctorCommand(
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
            .NotNull().WithMessage("Person data is required")
            .SetValidator(new CreatePersonRequestValidator());
    }
} 