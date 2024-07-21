using HospitalManagementSystem.Quartz.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Quartz.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddQuartzServices(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = new JobKey("CompleteAppointmentJob");

            q.AddJob<CompleteAppointmentJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("CompleteAppointmentJob-trigger")
                .WithCronSchedule("0 0/1 * * * ?"));
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}
