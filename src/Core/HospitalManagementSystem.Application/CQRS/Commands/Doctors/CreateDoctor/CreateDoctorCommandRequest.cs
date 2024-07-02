namespace HospitalManagementSystem.Application.CQRS.Commands.Doctors.CreateDoctor;

public record CreateDoctorCommandRequest(
    string Name,
    string Surname,
    string Address,
    string Phone,
    Guid DepartmentId) :IRequest<CreateDoctorCommandResponse>;