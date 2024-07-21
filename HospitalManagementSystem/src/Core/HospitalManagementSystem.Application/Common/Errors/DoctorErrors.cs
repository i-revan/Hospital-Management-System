namespace HospitalManagementSystem.Application.Common.Errors;

public static class DoctorErrors
{
    public static readonly Error DoctorNotFound = new("Doctors.NotFound", "No associated doctor found");
    //public static readonly Error DoctorAlreadyExist = new("Doctors.AlreadyExist", "This doctor already exists");
    public static readonly Error DoctorDepartmentDoesNotExist = new("Doctors.DepartmentDoesNotExist",
        "Selected department does not exist!");
    public static readonly Error DoctorNotAvailable = new("Doctors.Available", 
        "The doctor is not available at the selected time.");
    public static readonly Error DoctorCreationFailed = new("Doctors.CreationFailed", "Doctor creation failed");
    public static readonly Error DoctorUpdatingFailed = new("Doctors.UpdatingFailed", "Doctor updating failed");
    public static readonly Error DoctorDeletingFailed = new("Doctors.DeletingFailed", "Doctor deleting failed");
}
