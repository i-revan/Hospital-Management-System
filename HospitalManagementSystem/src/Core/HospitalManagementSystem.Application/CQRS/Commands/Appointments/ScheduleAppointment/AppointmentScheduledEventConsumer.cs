using MassTransit;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.ScheduleAppointment;

public sealed class AppointmentScheduledEventConsumer : IConsumer<AppointmentScheduledEvent>
{
    private readonly ILogger<AppointmentScheduledEventConsumer> _logger;

    public AppointmentScheduledEventConsumer(ILogger<AppointmentScheduledEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<AppointmentScheduledEvent> context)
    {
        _logger.LogInformation("Appointment scheduled: {@Appointment}", context.Message);

        return Task.CompletedTask;
    }
}
