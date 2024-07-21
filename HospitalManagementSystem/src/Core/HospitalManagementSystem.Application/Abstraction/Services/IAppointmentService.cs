using HospitalManagementSystem.Application.DTOs.Appointments;

namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IAppointmentService
{
    Task<ICollection<GetAllAppointmentsDto>> GetAllAsync();
    Task<Result<AppointmentItemDto>> GetByIdAsync(string id);
    Task<Result<bool>> ScheduleAppointmentAsync(ScheduleAppointmentDto dto);
    Task<Result<bool>> UpdateAppointmentAsync(string id, AppointmentUpdateDto dto);
    Task<Result<bool>> SoftDeleteAppointmentAsync(string id);
}
