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
}