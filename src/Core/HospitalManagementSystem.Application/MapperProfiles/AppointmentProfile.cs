using HospitalManagementSystem.Application.CQRS.Commands.Appointments.ScheduleAppointment;
using HospitalManagementSystem.Application.CQRS.Commands.Appointments.UpdateAppointment;
using HospitalManagementSystem.Application.DTOs.Appointments;

namespace HospitalManagementSystem.Application.MapperProfiles;

public class AppointmentProfile:Profile
{
    public AppointmentProfile()
    {
        CreateMap<Appointment, GetAllAppointmentsDto>()
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.Name} {src.User.Surname}"))
          .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.Name} {src.Doctor.Surname}"));

        CreateMap<Appointment, AppointmentItemDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name + " " + src.User.Surname))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name + " " + src.Doctor.Surname))
            .ReverseMap();

        CreateMap<AppointmentUpdateDto, Appointment>();
        CreateMap<ScheduleAppointmentDto, Appointment>();
        CreateMap<ScheduleAppointmentCommandRequest, ScheduleAppointmentDto>().ReverseMap();
        CreateMap<UpdateAppointmentCommandRequest, AppointmentUpdateDto>().ReverseMap();
    }
}
