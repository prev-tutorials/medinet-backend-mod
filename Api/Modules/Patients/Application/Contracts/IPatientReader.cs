
using Api.Modules.Patients.Application.Dtos;

namespace Api.Modules.Patients.Application.Contracts;

public interface IPatientReader
{
    Task<PatientResponse[]> GetByIdsAsync(
        Guid[] ids, 
        CancellationToken ct
    );
}