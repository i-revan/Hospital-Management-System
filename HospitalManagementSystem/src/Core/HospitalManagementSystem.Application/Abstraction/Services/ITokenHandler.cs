using HospitalManagementSystem.Domain.Entities.Identity;
using System.Security.Claims;

namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface ITokenHandler
{
    TokenResponseDto CreateJwt(AppUser user, IEnumerable<Claim> claims, int minutes);
}
