using Api.Modules.Doctors.Domain;
using Api.Modules.Doctors.Domain.ValueObjects;
using Api.Modules.Shared.Application.Contracts;

namespace Api.Modules.Doctors.Application.Contracts;

public interface IDoctorRepository:IRepository<Doctor>
{
    Task<Doctor[]> GetFilteredAsync(
        Guid? id,
        Speciality? speciality,
        string? search,
        CancellationToken ct
    );

    Task<Doctor[]> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids, 
        CancellationToken ct
    );
}