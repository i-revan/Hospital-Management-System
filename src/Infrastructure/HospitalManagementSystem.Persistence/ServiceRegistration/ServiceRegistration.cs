using HospitalManagementSystem.Application;
using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Persistence.Implementations.Repositories.Departments;
using HospitalManagementSystem.Persistence.Implementations.Repositories.Doctors;
using HospitalManagementSystem.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Persistence.ServiceRegistration;
public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 8;

            opt.User.RequireUniqueEmail = true;

            opt.Lockout.MaxFailedAccessAttempts = 3;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            opt.Lockout.AllowedForNewUsers = true;
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
        services.AddScoped<IDepartmentService, DepartmentService>();

        services.AddScoped<IDoctorService, DoctorService>();

        services.AddScoped<AppDbContextInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}