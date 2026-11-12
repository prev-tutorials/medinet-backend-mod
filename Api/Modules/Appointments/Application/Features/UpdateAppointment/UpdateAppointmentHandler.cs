using Api.Modules.Appointments.Application.Contracts;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.UpdateAppointment;

public class UpdateAppointmentHandler(
    IAppointmentRepository repository,
    IAppointmentService service
): IRequestHandler<UpdateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IAppointmentService _service = service;


    public async Task<Guid> Handle(
        UpdateAppointmentCommand command,
        CancellationToken ct
    )
    {
        var appointment = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException("Appointment with id not found");
        
        var request = command.Request;

        await _service.DoctorExist(request.DoctorId, ct);
        await _service.PatientExist(request.PatientId, ct);

        appointment.Update(
            request.DoctorId,
            request.PatientId,
            request.Date,
            request.Time,
            request.Reason,
            request.Status
        );

        var appointmentUpdated = await _repository.UpdateAsync(appointment, ct);
        return appointmentUpdated.Id;
    }
}
