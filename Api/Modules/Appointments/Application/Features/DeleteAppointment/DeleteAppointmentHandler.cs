using Api.Modules.Appointments.Application.Contracts;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.DeleteAppointment;

public class DeleteAppointmentHandler(
    IAppointmentRepository repository
): IRequestHandler<DeleteAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository = repository;

    public async Task<Guid> Handle(
        DeleteAppointmentCommand command,
        CancellationToken ct
    )
    {
        var appointment = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Appointment with id {command.Id} not found");

        var deleteDoctor = await _repository.DeleteAsync(appointment, ct);
        return deleteDoctor.Id;
    }
}
