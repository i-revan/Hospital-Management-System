namespace HospitalManagementSystem.Application.DTOs.Users;
public record LoginDto(
    string UserNameOrEmail,
    string Password);
