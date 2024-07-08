namespace HospitalManagementSystem.Application.Abstraction.Repositories.Appointments;
public interface IAppointmentReadRepository:IReadRepository<Appointment>
{
    Task<bool> IsDoctorAvailableAsync(Guid doctorId, DateTime startTime, DateTime endTime);
}
