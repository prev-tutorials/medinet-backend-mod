using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Domain;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.CreateAppointment;

public class CreateAppointmentHandler(
    IAppointmentRepository repository
): IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository = repository;

    public async Task<Guid> Handle(
        CreateAppointmentCommand command,
        CancellationToken ct
    )
    {
        var data = command.Request;
        
        var newAppointment = Appointment.Create(
            data.DoctorId,
            data.PatientId,
            DateOnly.Parse(data.Date),
            TimeOnly.Parse(data.Time),
            data.Reason
        );

        var result = await _repository.AddAsync(newAppointment, ct);
        return result.Id;
    }
}
