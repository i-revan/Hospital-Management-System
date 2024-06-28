using HospitalManagementSystem.Application.DTOs.Departments;

namespace HospitalManagementSystem.Application.Abstraction.Services
{
    public interface IDepartmentService
    {
        Task<ICollection<DepartmentItemDto>> GetAllAsync();
        Task<DepartmentItemDto> GetByIdAsync(int id);
        Task CreateAsync(DepartmentCreateDto dto);
        Task PutAsync(int id, DepartmentUpdateDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}
