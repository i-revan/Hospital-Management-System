namespace HospitalManagementSystem.Application.CQRS.Commands.Doctors.DeleteDoctor;
public record DeleteDoctorCommandRequest(string Id):IRequest<DeleteDoctorCommandResponse>;
