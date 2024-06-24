using HospitalManagementSystem.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Persistence.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Surname) 
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
