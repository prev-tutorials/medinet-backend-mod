using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Application.Dtos;
using Api.Modules.Doctors.Domain.ValueObjects;

namespace Api.Modules.Doctors.Infrastructure.Readers;

public class DoctorReader(
    IDoctorRepository repository
) : IDoctorReader
{
    private readonly IDoctorRepository _repository = repository;

    public async Task<DoctorResponse[]> GetByIdsAsync(
        Guid[] ids,
        CancellationToken ct
    )
    {
        if (ids.Length == 0) return [];

        var doctors = await _repository.GetByIdsAsync(ids, ct);

        return doctors
            .Select(d => d.ToResponse())
            .ToArray();
    }


    public async Task<DoctorResponse[]> GetBySpecialityAsync(
        string speciality,
        CancellationToken ct
    )
    {
        var doctors = await _repository.GetFilteredAsync(
            null,
            Speciality.From(speciality),
            null,
            ct
        );

        return doctors
            .Where(d => d.Active)
            .Select(d => d.ToResponse())
            .ToArray();
    }

}