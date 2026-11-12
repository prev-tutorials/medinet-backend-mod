using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Application.Exceptions;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.CreateAppointment;

public class CreateAppointmentHandler(
    IAppointmentRepository repository,
    IDoctorService doctorService,
    IPatientService patientService
): IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IDoctorService _doctorService = doctorService;
    private readonly IPatientService _patientService = patientService;


    public async Task<Guid> Handle(
        CreateAppointmentCommand command,
        CancellationToken ct
    )
    {
        var data = command.Request;

        await _doctorService.DoctorExist(data.DoctorId, ct);
        await _patientService.PatientExist(data.PatientId, ct);

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
