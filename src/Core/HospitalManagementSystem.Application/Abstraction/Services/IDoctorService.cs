namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IDoctorService
{
    Task<ICollection<DoctorItemDto>> GetAllAsync();
    Task<DoctorItemDto> GetByIdAsync(string id);
    Task<bool> CreateDoctorAsync(DoctorCreateDto dto);
    Task<bool> UpdateDoctorAsync(string id, DoctorUpdateDto dto);
    Task<bool> DeleteDoctorAsync(string id);
    Task<bool> SoftDeleteDoctorAsync(string id);
}