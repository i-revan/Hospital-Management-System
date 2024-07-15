using HospitalManagementSystem.Application.CQRS.Commands.Doctors.CreateDoctor;
using HospitalManagementSystem.Application.CQRS.Commands.Doctors.UpdateDoctor;

namespace HospitalManagementSystem.Application.MapperProfiles;

public class DoctorProfile:Profile
{
    public DoctorProfile()
    {
        CreateMap<Doctor, DoctorItemDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name)).ReverseMap();
        CreateMap<DoctorCreateDto, Doctor>();
        CreateMap<DoctorUpdateDto, Doctor>();
        CreateMap<CreateDoctorCommandRequest, DoctorCreateDto>().ReverseMap();
        CreateMap<UpdateDoctorCommandRequest, DoctorUpdateDto>().ReverseMap();
    }
}
