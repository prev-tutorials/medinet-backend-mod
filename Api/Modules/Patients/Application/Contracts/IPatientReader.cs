
using Api.Modules.Patients.Application.Dtos;

namespace Api.Modules.Patients.Application.Contracts;

public interface IPatientReader
{
    Task<IReadOnlyDictionary<Guid, PatientResponse>> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids, 
        CancellationToken ct
    );
}