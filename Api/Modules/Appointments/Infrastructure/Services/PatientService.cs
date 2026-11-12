using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Application.Dtos;
using Api.Modules.Patients.Application.Contracts;
using FluentValidation;
using FluentValidation.Results;

namespace Api.Modules.Appointments.Infrastructure.Services;

public class PatientService(
    IPatientReader patientReader
) : IPatientService
{
    private readonly IPatientReader _patientReader = patientReader;

    public async Task<PatientResponse[]> GetPatientsByIdAsync(
        Guid[] ids, 
        CancellationToken ct
    )
    {
        var patients =  await _patientReader.GetByIdsAsync(ids, ct);
        return patients.Select(d => new PatientResponse(
            d.Id,
            d.Nhc,
            d.Person
        )).ToArray();
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
                throw new ValidationException(
                    [new ValidationFailure("PatientId", "Patient does not exist")]
                );
        }
    }

}