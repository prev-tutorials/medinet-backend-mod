using Api.Modules.Appointments.Application.Dtos;

namespace Api.Modules.Appointments.Application.Contracts;

public interface IDoctorService
{
    Task<DoctorResponse[]> GetDoctorsByIdAsync(
        Guid[] ids,
        CancellationToken ct
    );

    Task DoctorExist(
        Guid? doctorId,
        CancellationToken ct
    );
}
