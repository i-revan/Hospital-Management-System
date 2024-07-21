namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IDoctorService
{
    Task<ICollection<DoctorItemDto>> GetAllAsync();
    Task<Result<DoctorItemDto>> GetByIdAsync(string id);
    Task<Result<bool>> CreateDoctorAsync(DoctorCreateDto dto);
    Task<Result<bool>> UpdateDoctorAsync(string id, DoctorUpdateDto dto);
    Task<Result<bool>> DeleteDoctorAsync(string id);
    Task<Result<bool>> SoftDeleteDoctorAsync(string id);
}