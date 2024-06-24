using FluentValidation;
using FluentValidation.AspNetCore;
using HospitalManagementSystem.Application.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HospitalManagementSystem.Application.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(DepartmentProfile));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
