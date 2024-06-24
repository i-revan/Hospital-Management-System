using HospitalManagementSystem.Application.DTOs.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Abstraction.Services
{
    public interface IDepartmentService
    {
        Task<ICollection<DepartmentItemDto>> GetAllAsync();
        //Task<GetDepartmentDto> GetByIdAsync(int id);
        Task CreateAsync(DepartmentCreateDto dto);
        //Task<UpdateDepartmentDto> UpdateAsync(int id, UpdateDepartmentDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}
