namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IDepartmentService
{
    Task<ICollection<AllDepartmentsDto>> GetAllAsync();
    Task<Result<DepartmentItemDto>> GetByIdAsync(string id);
    Task<Result<bool>> CreateDepartmentAsync(DepartmentCreateDto dto);
    Task<Result<bool>> UpdateDepartmentAsync(string id, DepartmentUpdateDto dto);
    Task<Result<bool>> DeleteDepartmentAsync(string id);
    Task<Result<bool>> SoftDeleteDepartmentAsync(string id);
}