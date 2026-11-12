using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Application.Exceptions;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.UpdateAppointment;

public class UpdateAppointmentHandler(
    IAppointmentRepository repository,
    IDoctorService doctorService,
    IPatientService patientService
): IRequestHandler<UpdateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IDoctorService _doctorService = doctorService;
    private readonly IPatientService _patientService = patientService;

    public async Task<Guid> Handle(
        UpdateAppointmentCommand command,
        CancellationToken ct
    )
    {
        var appointment = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException("Appointment with id not found");
        
        var request = command.Request;

        await _doctorService.DoctorExist(request.DoctorId, ct);
        await _patientService.PatientExist(request.PatientId, ct);

        appointment.Update(
            request.DoctorId,
            request.PatientId,
            request.Date,
            request.Time,
            request.Reason,
            request.Status
        );

        if (appointment.Status != AppointmentStatus.Cancelled)
        {
            var hasConflict = await _repository.HasConflictAsync(
                appointment.DoctorId, 
                appointment.Date, 
                appointment.Time, 
                appointment.Id, 
                ct
            );

            if(hasConflict)
                throw new ConflictException("Doctor already has an appointment at that date and time");
        }

        var appointmentUpdated = await _repository.UpdateAsync(appointment, ct);
        return appointmentUpdated.Id;
    }
}
