using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Application.Contracts;

namespace Api.Modules.Appointments.Application.Contracts;


public interface IAppointmentRepository : IRepository<Appointment>
{

    Task<Appointment[]> GetFilteredAsync(
        Guid? id,
        Guid? doctorId,
        Guid? patientId,
        string? search,
        CancellationToken ct
    );

    Task<Appointment[]> GetByDoctorsAndDateAsync(
        Guid[] doctorIds,
        DateOnly date,
        CancellationToken ct
    );

    Task<bool> HasConflictAsync(
        Guid doctorId,
        DateOnly date,
        TimeOnly time,
        Guid? excludeId,
        CancellationToken ct
    );

}