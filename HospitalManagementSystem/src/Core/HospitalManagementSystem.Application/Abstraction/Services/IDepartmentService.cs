namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IDepartmentService
{
    Task<ICollection<AllDepartmentsDto>> GetAllAsync();
    Task<DepartmentItemDto> GetByIdAsync(string id);
    Task<bool> CreateDepartmentAsync(DepartmentCreateDto dto);
    Task<bool> UpdateDepartmentAsync(string id, DepartmentUpdateDto dto);
    Task<bool> DeleteDepartmentAsync(string id);
    Task<bool> SoftDeleteDepartmentAsync(string id);
}