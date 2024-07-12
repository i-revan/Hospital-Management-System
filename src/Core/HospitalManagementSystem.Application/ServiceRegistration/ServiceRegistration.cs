using FluentValidation.AspNetCore;
using HospitalManagementSystem.Application.Behaviors;
using HospitalManagementSystem.Application.Stripe;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HospitalManagementSystem.Application.ServiceRegistration;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddAutoMapper(typeof(DepartmentProfile));
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>),
            typeof(LoggingPipelineBehavior<,>));

        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssembly(assembly);

        return services;
    }
}
