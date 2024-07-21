namespace HospitalManagementSystem.Application.Common.Errors
{
    public static class DepartmentErrors
    {
        public static readonly Error DepartmentNotFound = new("Departments.NotFound", "No associated department found");
        public static readonly Error DepartmentAlreadyExist = new("Departments.AlreadyExist", "This department already exists");
        public static readonly Error DepartmentCreationFailed = new("Departments.CreationFailed", "Department creation failed");
        public static readonly Error DepartmentUpdatingFailed = new("Departments.UpdatingFailed", "Department updating failed");
        public static readonly Error DepartmentDeletingFailed = new("Departments.DeletingFailed", "Department deleting failed");
    }
}
