using HospitalManagementSystem.Application.DTOs.Appointments;

namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IAppointmentService
{
    Task<ICollection<GetAllAppointmentsDto>> GetAllAsync();
    Task<AppointmentItemDto> GetByIdAsync(string id);
    Task<bool> ScheduleAppointmentAsync(ScheduleAppointmentDto dto);
    Task<bool> UpdateAppointmentAsync(string id, AppointmentUpdateDto dto);
    Task<bool> SoftDeleteAppointmentAsync(string id);
}
