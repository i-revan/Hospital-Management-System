namespace HospitalManagementSystem.Application.Common.Errors;

public static class AppointmentErrors
{
    public static readonly Error AppointmentNotFound = new("Appointment.NotFound", "No associated appointment found");
    public static readonly Error AppointmentWithSameDoctor = new("Appointment.WithSameDoctor", 
        "You already have an appointment with this doctor on the same day.");
    public static readonly Error AppointmentOverlap = new("Appointment.Overlap",
        "You have another appointment at the selected time.");
    public static readonly Error AppointmentCancel = new("Appointment.Cancel", 
        "Appointments cannot be canceled within 5 hours of the start time.");
    public static readonly Error AppointmentScheduleFailed = new("Appointments.ScheduleFailed", "Appointment schedule failed");
    public static readonly Error AppointmentUpdatingFailed = new("Appointments.UpdatingFailed", "Appointment updating failed");
    public static readonly Error AppointmentDeletingFailed = new("Appointments.DeletingFailed", "Appointment deleting failed");
}
