using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Domain;
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

}