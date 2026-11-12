using Api.Modules.Appointments.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Appointments.Infrastructure.Persistence;


public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("appointments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DoctorId).IsRequired();
        builder.Property(x => x.PatientId).IsRequired();

        builder.HasIndex(x => new { x.DoctorId, x.Date, x.Time })
            .IsUnique()
            .HasFilter("[Status] <> 3 AND [IsDeleted] = 0");

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Time)
            .HasColumnType("time(0)")
            .IsRequired();

        builder.Property(x => x.Reason).HasMaxLength(500);
    }
}