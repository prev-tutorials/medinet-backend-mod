using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Application.Dtos;

namespace Api.Modules.Doctors.Infrastructure.Readers;

public class DoctorReader(
    IDoctorRepository repository
) : IDoctorReader
{
    private readonly IDoctorRepository _repository = repository;

    public async Task<IReadOnlyDictionary<Guid, DoctorResponse>> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken ct
    )
    {
        if (ids.Count == 0)
            return new Dictionary<Guid, DoctorResponse>();

        var doctors = await _repository.GetByIdsAsync(ids, ct);

        return doctors.ToDictionary(d => d.Id, d => d.ToResponse());
    }
}