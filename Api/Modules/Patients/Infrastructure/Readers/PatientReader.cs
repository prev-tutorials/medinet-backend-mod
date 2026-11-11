using Api.Modules.Patients.Application.Contracts;
using Api.Modules.Patients.Application.Dtos;

namespace Api.Modules.Patients.Infrastructure.Readers;

public class PatientReader(
    IPatientRepository repository
) : IPatientReader
{
    private readonly IPatientRepository _repository = repository;

    public async Task<IReadOnlyDictionary<Guid, PatientResponse>> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken ct
    )
    {
        if (ids.Count == 0)
            return new Dictionary<Guid, PatientResponse>();

        var patients = await _repository.GetByIdsAsync(ids, ct);

        return patients.ToDictionary(d => d.Id, d => d.ToResponse());
    }
}