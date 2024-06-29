namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IDepartmentService
{
    Task<ICollection<DepartmentItemDto>> GetAllAsync();
    Task<DepartmentItemDto> GetByIdAsync(int id);
    Task<bool> CreateDepartmentAsync(DepartmentCreateDto dto);
    Task<bool> UpdateDepartmentAsync(int id, DepartmentUpdateDto dto);
    Task<bool> DeleteDepartmentAsync(int id);
    Task<bool> SoftDeleteDepartmentAsync(int id);
}