using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Domain;
using Api.Modules.Doctors.Domain.ValueObjects;
using Api.Modules.Shared.Infrastructure.Persistence;
using Api.Modules.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Doctors.Infrastructure.Repository;


public class DoctorRepository(AppDbContext context)
    :Repository<Doctor>(context), IDoctorRepository
{
    
    protected override IQueryable<Doctor> Query()
        => _context.Set<Doctor>()
            .Include(d => d.Person)
            .ThenInclude(p => p.PersonContact);



    public async Task<Doctor[]> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids, 
        CancellationToken ct
    )=> await Query()
            .Where(d => ids.Contains(d.Id))
            .ToArrayAsync(ct);



    public async Task<Doctor[]> GetFilteredAsync(
        Guid? id, 
        Speciality? speciality, 
        string? search, 
        CancellationToken ct
    )
    {
        var query = Query().Where(d => !d.IsDeleted);

        if(id != null)
            query = query.Where(d => d.Id == id);
        
        if(speciality != null)
            query = query.Where(d => d.Speciality == speciality);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();

            query = query.Where(
                d =>
                d.NRegister.Contains(term) ||
                d.Person.Dni.Contains(term) ||
                d.Person.FirstName.Contains(term) ||
                d.Person.LastName.Contains(term)
            );
        }

        return await query.ToArrayAsync(ct);
    }
}