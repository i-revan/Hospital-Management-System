using HospitalManagementSystem.Application.CQRS.Commands.Departments.CreateDepartment;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;

namespace HospitalManagementSystem.Application.MapperProfiles;
internal class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentItemDto>().ReverseMap();
        CreateMap<DepartmentCreateDto, Department>();
        CreateMap<DepartmentUpdateDto, Department>();
        CreateMap<CreateDepartmentCommandRequest, DepartmentCreateDto>().ReverseMap();
        CreateMap<UpdateDepartmentCommandRequest, DepartmentUpdateDto>().ReverseMap();
    }
}
