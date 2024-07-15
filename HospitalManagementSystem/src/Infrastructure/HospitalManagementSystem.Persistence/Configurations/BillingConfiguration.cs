namespace HospitalManagementSystem.Persistence.Configurations;

public class BillingConfiguration : IEntityTypeConfiguration<Billing>
{
    public void Configure(EntityTypeBuilder<Billing> builder)
    {
        builder.HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.AppointmentId).IsRequired();
        builder.Property(b => b.Amount)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}
