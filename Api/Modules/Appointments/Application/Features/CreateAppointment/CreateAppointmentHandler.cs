using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Application.Exceptions;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.CreateAppointment;

public class CreateAppointmentHandler(
    IAppointmentRepository repository,
    IAppointmentService service
): IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IAppointmentService _service = service;

    public async Task<Guid> Handle(
        CreateAppointmentCommand command,
        CancellationToken ct
    )
    {
        var data = command.Request;

        await _service.DoctorExist(data.DoctorId, ct);
        await _service.PatientExist(data.PatientId, ct);

        var hasConflict = await _repository.HasConflictAsync(
            data.DoctorId, 
            data.Date, 
            data.Time, 
            null, 
            ct
        );

        if(hasConflict)
            throw new ConflictException("Doctor already has an appointment at that date and time");
        
        var newAppointment = Appointment.Create(
            data.DoctorId,
            data.PatientId,
            data.Date,
            data.Time,
            data.Reason
        );

        var result = await _repository.AddAsync(newAppointment, ct);
        return result.Id;
    }

}
