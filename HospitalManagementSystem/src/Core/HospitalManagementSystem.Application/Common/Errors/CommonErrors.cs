namespace HospitalManagementSystem.Application.Common.Errors;

public static class CommonErrors
{
    public static readonly Error InvalidId = new("Common.InvalidId", "Id cannot be null");
}
