using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Application.Dtos;
using Api.Modules.Patients.Application.Contracts;
using Api.Modules.Patients.Application.Dtos;
using FluentValidation;
using FluentValidation.Results;

namespace Api.Modules.Appointments.Application.Services;

public class AppointmentService(
    IDoctorReader doctorReader,
    IPatientReader patientReader
) : IAppointmentService
{
    private readonly IDoctorReader _doctorReader = doctorReader;
    private readonly IPatientReader _patientReader = patientReader;


    public async Task<DoctorResponse[]> GetDoctorsByIdAsync(
        Guid[] ids, 
        CancellationToken ct
    ) =>  await _doctorReader.GetByIdsAsync(ids, ct);

    public async Task<PatientResponse[]> GetPatientsByIdAsync(
        Guid[] ids, 
        CancellationToken ct
    ) => await _patientReader.GetByIdsAsync(ids, ct);


    public async Task DoctorExist(
        Guid? doctorId,
        CancellationToken ct
    )
    {
        if (doctorId.HasValue)
        {
            var did = doctorId.Value;
            var doctors = await _doctorReader.GetByIdsAsync([did], ct);

            if (doctors.Length == 0)
            //    throw new ValidationException("Error: doctor with id not exist");
                throw new ValidationException(
                    [new ValidationFailure("DoctorId", "Doctor does not exist")]
                );
        }
    }

    

    public async Task PatientExist(
        Guid? patientId,
        CancellationToken ct
    )
    {
        if (patientId.HasValue)
        {
            var pid = patientId.Value;
            var patients = await _patientReader.GetByIdsAsync([pid], ct);
            
            if (patients.Length == 0)
                throw new ValidationException("Error: patient with id not exist");
        }
    }
}