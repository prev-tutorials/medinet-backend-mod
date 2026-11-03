using Api.Modules.Appointments.Application.Contracts;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.UpdateAppointment;

public class UpdateAppointmentHandler(
    IAppointmentRepository repository
): IRequestHandler<UpdateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository = repository;

    public async Task<Guid> Handle(
        UpdateAppointmentCommand command,
        CancellationToken ct
    )
    {
        var appointment = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new ArgumentException("Appointment with id not found");

        var appointmentUpdated = await _repository.UpdateAsync(appointment, ct);
        return appointmentUpdated.Id;
    }
}
