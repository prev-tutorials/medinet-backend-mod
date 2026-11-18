using Api.Modules.Patients.Application.Contracts;
using Api.Modules.Patients.Domain;
using Api.Modules.Shared.Infrastructure.Persistence;
using Api.Modules.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Patients.Infrastructure.Repositories;

public class PatientRepository(AppDbContext context)
    :Repository<Patient>(context), IPatientRepository
{
    protected override IQueryable<Patient> Query()
        => _context.Set<Patient>()
            .Include(d => d.Person)
            .ThenInclude(p => p.PersonContact);


    public async Task<Patient[]> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids, 
        CancellationToken ct
    )
        => await Query()
            .Where(d => ids.Contains(d.Id))
            .ToArrayAsync(ct);


    public async Task<Patient[]> GetFilteredAsync(
        Guid? id,
        string? dni,
        string? search,
        CancellationToken ct
    )
    {
        var query = Query().Where(p => !p.IsDeleted);

        if(id != null)
            query = query.Where(p => p.Id == id);

        if (!string.IsNullOrWhiteSpace(dni))
            query = query.Where(p => p.Person.Dni == dni.Trim());

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();

            query = query.Where(
                p =>
                p.Person.Dni.Contains(term) ||
                p.Person.FirstName.Contains(term) ||
                p.Person.LastName.Contains(term)
            );
        }

        return await query.ToArrayAsync(ct);
    }
}