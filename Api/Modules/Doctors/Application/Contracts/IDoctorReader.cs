using Api.Modules.Doctors.Application.Dtos;

namespace Api.Modules.Doctors.Application.Contracts;

public interface IDoctorReader
{
    Task<IReadOnlyDictionary<Guid, DoctorResponse>> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids, 
        CancellationToken ct
    );
}