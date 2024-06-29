namespace HospitalManagementSystem.Persistence.Configurations;
internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
        builder.Property(d => d.Surname).IsRequired().HasMaxLength(50);
        builder.Property(d => d.Address).IsRequired().HasMaxLength(100);
    }
}
