using HospitalManagementSystem.Application;
using HospitalManagementSystem.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Quartz.Jobs;

public class CompleteAppointmentJob : IJob
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CompleteAppointmentJob> _logger;

    public CompleteAppointmentJob(IUnitOfWork unitOfWork, ILogger<CompleteAppointmentJob> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var appointments = _unitOfWork.AppointmentReadRepository
            .GetAllWhere(a => a.EndTime <= DateTime.UtcNow && a.Status != AppointmentStatus.Completed);

        foreach (var appointment in appointments)
        {
            appointment.Status = AppointmentStatus.Completed;
            _unitOfWork.AppointmentWriteRepository.Update(appointment);
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("CompleteAppointmentJob successfully executed at {Time}", DateTime.UtcNow);
    }
}
