using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.CreateDoctor;


public record CreateDoctorRequest(
    string Speciality,
    string NRegister,
    string Dni,
    string FirstName,
    string LastName,
    string? BirthDate,
    string? Phone,
    string? Email,
    string? Address
);

public record CreateDoctorCommand(
    Guid Id,
    string Speciality,
    string NRegister,
    string Dni,
    string FirstName,
    string LastName,
    string? BirthDate,
    string? Phone,
    string? Email,
    string? Address
):IRequest<Guid>;


public class CreateDoctorValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.NRegister)
            .NotEmpty().WithMessage("NRegister is required")
            .MaximumLength(150);

        RuleFor(x => x.Speciality)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("Dni is required")
            .MinimumLength(10)
            .MaximumLength(15).WithMessage("Dni must be 15 characters or less");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(15)
            .When(x => x.Phone != null);
    }
} 