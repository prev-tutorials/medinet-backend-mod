using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Application.Contracts;

namespace Api.Modules.Appointments.Application.Contracts;


public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<bool> HasConflictAsync(
        Guid doctorId,
        DateOnly date,
        TimeOnly time,
        Guid? excludeId,
        CancellationToken ct
    );

}