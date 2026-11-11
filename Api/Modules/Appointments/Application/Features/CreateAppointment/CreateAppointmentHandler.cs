using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Domain;
using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Patients.Application.Contracts;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.CreateAppointment;

public class CreateAppointmentHandler(
    IAppointmentRepository repository,
    IDoctorReader doctorReader,
    IPatientReader patientReader
): IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IDoctorReader _doctorReader = doctorReader;
    private readonly IPatientReader _patientReader = patientReader;

    public async Task<Guid> Handle(
        CreateAppointmentCommand command,
        CancellationToken ct
    )
    {
        var data = command.Request;

         await EnsureReferencesExist(
            data.DoctorId, 
            data.PatientId, 
            ct
        );
        
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





    private async Task EnsureReferencesExist(
        Guid doctorId,
        Guid patientId,
        CancellationToken ct
    )
    {
        var doctors  = await _doctorReader.GetByIdsAsync([doctorId], ct);
        var patients = await _patientReader.GetByIdsAsync([patientId], ct);

        var failures = new List<ValidationFailure>();

        if (!doctors.ContainsKey(doctorId))
            failures.Add(new ValidationFailure("DoctorId", "Doctor does not exist"));

        if (!patients.ContainsKey(patientId))
            failures.Add(new ValidationFailure("PatientId", "Patient does not exist"));

        if (failures.Count != 0)
            throw new ValidationException(failures);
    }
}
