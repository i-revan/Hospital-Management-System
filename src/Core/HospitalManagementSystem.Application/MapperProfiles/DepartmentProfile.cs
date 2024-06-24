using AutoMapper;
using HospitalManagementSystem.Application.DTOs.Departments;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.MapperProfiles
{
    internal class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentItemDto>().ReverseMap();
            CreateMap<DepartmentCreateDto, Department>();
        }
    }
}
