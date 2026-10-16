using Api.Modules.Doctors.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Doctors.Infrastructure.Persistence;


public class DoctorConfiguracion : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("doctors");
        builder.HasKey(x => x.Id);

        //conf relacion con person
        builder.HasOne(x => x.Person)
            .WithOne()
            .HasForeignKey<Doctor>(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PersonId).IsUnique();
    }
}