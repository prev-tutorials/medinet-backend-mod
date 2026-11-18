using Api.Modules.Doctors.Application.Dtos;
using Api.Modules.Doctors.Domain.ValueObjects;

namespace Api.Modules.Doctors.Application.Contracts;

public interface IDoctorReader
{
    Task<DoctorResponse[]> GetByIdsAsync(
        Guid[] ids, 
        CancellationToken ct
    );

    Task<DoctorResponse[]> GetFilteredAsync(
        Guid? id,
        Speciality? speciality,
        string? search,
        CancellationToken ct
    );
}