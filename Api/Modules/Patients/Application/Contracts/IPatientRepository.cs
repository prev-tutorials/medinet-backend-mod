using Api.Modules.Patients.Domain;
using Api.Modules.Shared.Application.Contracts;

namespace Api.Modules.Patients.Application.Contracts;

public interface IPatientRepository:IRepository<Patient>
{
    Task<Patient[]> GetFilteredAsync(
        Guid? id,
        string? dni,
        string? search,
        CancellationToken ct
    );


    Task<Patient[]> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids, 
        CancellationToken ct
    );
}