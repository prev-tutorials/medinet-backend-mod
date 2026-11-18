using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Infrastructure.Persistence;
using Api.Modules.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Appointments.Infrastructure.Repositories;


public class AppointmentRepository(
    AppDbContext context
) : Repository<Appointment>(context), IAppointmentRepository
{
    public async Task<Appointment[]> GetFilteredAsync(
        Guid? id, 
        Guid? doctorId, 
        Guid? patientId, 
        string? search, 
        CancellationToken ct
    )
    {
        var query = Query().Where(a => !a.IsDeleted);

        if (id != null)        query = query.Where(a => a.Id == id);
        if (doctorId != null)  query = query.Where(a => a.DoctorId == doctorId);
        if (patientId != null) query = query.Where(a => a.PatientId == patientId);
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(a => a.Reason != null && a.Reason.Contains(term));
        }

        return await query.ToArrayAsync(ct);
    }


    public async Task<Appointment[]> GetByDoctorsAndDateAsync(
        Guid[] doctorIds,
        DateOnly date,
        CancellationToken ct
    )
    {
        if (doctorIds.Length == 0)
            return [];

        return await Query()
            .Where(a =>
                !a.IsDeleted
                && a.Status != AppointmentStatus.Cancelled
                && a.Date == date
                && doctorIds.Contains(a.DoctorId)
            )
            .ToArrayAsync(ct);
    }


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