using Api.Modules.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Shared.Infrastructure.Persistence;


public class PersonConfiguracion 
: IEntityTypeConfiguration<Person>, IEntityTypeConfiguration<PersonContact>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Dni).IsUnique();
        builder.Property(p => p.Dni).HasMaxLength(15);
    }


    public void Configure(EntityTypeBuilder<PersonContact> builder)
    {
        builder.ToTable("person_contacts");
        builder.HasKey(p => p.Id);
    }
}