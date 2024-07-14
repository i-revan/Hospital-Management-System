using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginByRefresh;
using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginUser;
using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.RegisterUser;
using HospitalManagementSystem.Domain.Entities.Identity;

namespace HospitalManagementSystem.Application.MapperProfiles;
internal class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<RegisterDto, AppUser>();
        CreateMap<RegisterUserCommandRequest, RegisterDto>().ReverseMap();
        CreateMap<LoginUserCommandRequest, LoginDto>().ReverseMap();
        CreateMap<LoginUserCommandResponse, TokenResponseDto>().ReverseMap();
        CreateMap<LoginByRefreshCommandResponse, TokenResponseDto>().ReverseMap();
    }
}
