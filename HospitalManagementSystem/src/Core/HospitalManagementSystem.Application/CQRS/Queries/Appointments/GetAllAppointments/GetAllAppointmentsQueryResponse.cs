using HospitalManagementSystem.Application.DTOs.Appointments;

namespace HospitalManagementSystem.Application.CQRS.Queries.Appointments.GetAllAppointments;

public record GetAllAppointmentsQueryResponse(List<GetAllAppointmentsDto> Appointments);
