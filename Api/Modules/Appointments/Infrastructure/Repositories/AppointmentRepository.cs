using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Application.Exceptions;
using Api.Modules.Shared.Infrastructure.Persistence;
using Api.Modules.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Appointments.Infrastructure.Repositories;


public class AppointmentRepository(
    AppDbContext context
) : Repository<Appointment>(context), IAppointmentRepository
{
    public Task<bool> HasConflictAsync(
        Guid doctorId, 
        DateOnly date, 
        TimeOnly time, 
        Guid? excludeId, 
        CancellationToken ct
    )
    {
        return Query().AnyAsync(
            a => a.DoctorId == doctorId
            && a.Date   == date
            && a.Time   == time
            && a.Status != AppointmentStatus.Cancelled
            && (excludeId == null || a.Id != excludeId),
            ct
        );
    }

}