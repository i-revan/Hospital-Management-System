
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Persistence.Configurations;

internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasOne(a => a.User)
               .WithMany()
               .HasForeignKey(a => a.UserId)
               .IsRequired();
        builder.Property(a => a.StartTime).IsRequired();
        builder.Property(a => a.EndTime).IsRequired();
        builder.Property(a => a.Remarks).HasMaxLength(500).IsRequired(false);
    }
}
