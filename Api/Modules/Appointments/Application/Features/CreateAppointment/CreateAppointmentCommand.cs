using Api.Modules.Shared.Application.Helpers;
using FluentValidation;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.CreateAppointment;

public record CreateAppointmentRequest(
    Guid DoctorId,
    Guid PatientId,
    string Date,
    string Time,
    string? Reason
);

public record CreateAppointmentCommand(
    CreateAppointmentRequest Request
) : IRequest<Guid>;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.Request)
            .NotNull();

        RuleFor(x => x.Request.DoctorId)
            .NotEmpty();

        RuleFor(x => x.Request.PatientId)
            .NotEmpty();

        RuleFor(x => x.Request.Date)
            .NotEmpty().WithMessage("Date is required")
            .Must(DateHelper.IsValidStringDate)
            .WithMessage("Date must be in format yyyy/mm/dd");

        RuleFor(x => x.Request.Time)
            .NotEmpty().WithMessage("Time is required")
            .Must(TimeHelper.IsValidStringTime)
            .WithMessage("Time must be in format HH:mm:ss");

        RuleFor(x => x.Request.Reason)
            .MaximumLength(500);
    }
}
