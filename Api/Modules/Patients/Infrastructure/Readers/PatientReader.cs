using Api.Modules.Patients.Application.Contracts;
using Api.Modules.Patients.Application.Dtos;

namespace Api.Modules.Patients.Infrastructure.Readers;

public class PatientReader(
    IPatientRepository repository
) : IPatientReader
{
    private readonly IPatientRepository _repository = repository;

    public async Task<PatientResponse[]> GetByIdsAsync(
        Guid[] ids,
        CancellationToken ct
    )
    {
        if (ids.Length == 0) return [];

        var patients = await _repository.GetByIdsAsync(ids, ct);

        return patients
            .Select(d => d.ToResponse())
            .ToArray();
    }
}