﻿using HospitalManagementSystem.Application.DTOs.Appointments;

namespace HospitalManagementSystem.Application.CQRS.Queries.Appointments.GetAppointmentById;

public record GetAppointmentByIdQueryResponse(AppointmentItemDto Appointment);
