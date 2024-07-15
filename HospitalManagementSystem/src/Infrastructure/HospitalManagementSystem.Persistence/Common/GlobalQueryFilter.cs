namespace HospitalManagementSystem.Persistence.Common;
internal static class GlobalQueryFilter
{
    public static void ApplyFilter<T>(this ModelBuilder modelBuilder) where T : BaseEntity, new()
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => e.IsDeleted == false);
    }
    public static void ApplyFilters(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyFilter<Department>();
        modelBuilder.ApplyFilter<Doctor>();
        modelBuilder.ApplyFilter<Appointment>();
        modelBuilder.ApplyFilter<Billing>();
    }
}
