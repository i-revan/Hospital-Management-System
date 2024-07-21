namespace HospitalManagementSystem.Application.CQRS.Commands.Doctors.UpdateDoctor;
public record UpdateDoctorCommandRequest(
    string Id,
    string Name,
    string Surname,
    string Address,
    string Phone,
    Guid DepartmentId
    ):IRequest<UpdateDoctorCommandResponse>;
