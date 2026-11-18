using Api.Modules.Doctors.Application.Dtos;

namespace Api.Modules.Doctors.Application.Contracts;

public interface IDoctorReader
{
    Task<DoctorResponse[]> GetByIdsAsync(
        Guid[] ids, 
        CancellationToken ct
    );

    Task<DoctorResponse[]> GetBySpecialityAsync(
        string speciality,
        CancellationToken ct
    );
}