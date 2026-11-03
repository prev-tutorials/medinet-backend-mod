using Api.Modules.Appointments.Domain;
using Api.Modules.Doctors.Domain;
using Api.Modules.Patients.Domain;
using Api.Modules.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Shared.Infrastructure.Persistence;


public class AppDbContext(
    DbContextOptions<AppDbContext> options
    // IHttpContextAccessor httpContextAccesor
) : DbContext(options)
{
    // private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccesor;

    public DbSet<Person> People => Set<Person>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly
        );

        modelBuilder.ApplySoftDeleteFilter();
    }



    public override int SaveChanges()
    {
        ApplyAudit();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct)
    {
        ApplyAudit();
        return base.SaveChangesAsync(ct);
    }


    //todo usar identity para obtener el usuaria
    private void ApplyAudit()
    {
        var now = DateTime.UtcNow;

        foreach(var entry in ChangeTracker.Entries<Entity>())
        {
            if(entry.State is not(EntityState.Added or EntityState.Modified))
                continue;
            
            entry.Entity.UpdatedAt = now;
            // entry.Entity.UpdatedBy = ;

            if(entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                // entry.Entity.CreatedBy = ;
            }
        }
    }

}