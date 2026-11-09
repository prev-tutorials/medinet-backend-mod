using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Application.Helpers;
using FluentValidation;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.UpdateAppointment;

public record UpdateAppointmentRequest(
    Guid? DoctorId,
    Guid? PatientId,
    DateOnly? Date,
    TimeOnly? Time,
    string? Reason,
    AppointmentStatus? Status
);

public record UpdateAppointmentCommand(
    Guid Id,
    UpdateAppointmentRequest Request
) : IRequest<Guid>;


public class UpdateAppointmentValidator : AbstractValidator<UpdateAppointmentCommand>
{
    public UpdateAppointmentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Request)
            .NotNull();

        RuleFor(x => x.Request.DoctorId)
            .NotEmpty()
            .WithMessage("Doctor Id cannot be empty or whitespace")
            .When(x => x.Request.DoctorId != null);

        RuleFor(x => x.Request.PatientId)
            .NotEmpty()
            .WithMessage("Patient Id cannot be empty or whitespace")
            .When(x => x.Request.PatientId != null);

        RuleFor(x => x.Request.Date)
            .NotEmpty().WithMessage("Date is required")
            .WithMessage("Date must be in format yyyy-mm-dd")
            .When(x => x.Request.Date != null);

        RuleFor(x => x.Request.Time)
            .NotEmpty().WithMessage("Time is required")
            .WithMessage("Time must be in format HH:mm:ss")
            .When(x => x.Request.Time != null);
    }
}
