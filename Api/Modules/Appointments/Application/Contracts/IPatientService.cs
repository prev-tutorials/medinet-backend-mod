using Api.Modules.Appointments.Application.Dtos;

namespace Api.Modules.Appointments.Application.Contracts;

public interface IPatientService
{

    Task<PatientResponse[]> GetPatientsByIdAsync(
        Guid[] ids,
        CancellationToken ct 
    );


    Task PatientExist(
        Guid? patientId,
        CancellationToken ct
    );
}