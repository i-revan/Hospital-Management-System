using HospitalManagementSystem.Application.DTOs.Doctors;

namespace HospitalManagementSystem.Application.Abstraction.Services
{
    public interface IDoctorService
    {
        Task<ICollection<DoctorItemDto>> GetAllAsync();
        Task<DoctorItemDto> GetByIdAsync(int id);
        Task CreateAsync(DoctorCreateDto dto);
        Task PutAsync(int id, DoctorUpdateDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}
