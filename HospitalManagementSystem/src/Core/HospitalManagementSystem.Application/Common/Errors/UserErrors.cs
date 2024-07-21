namespace HospitalManagementSystem.Application.Common.Errors;

public static class UserErrors
{
    public static readonly Error UserNotFound = new("User.NotFound", "User not found");
    public static readonly Error UserAlreadyExist = new("User.AlreadyExist", "User already exist");
    public static readonly Error UserLoginFailed = new("User.LoginFailed", "Username/Email or password is incorrect");
    public static readonly Error UserRefreshTokenExpiration = new("User.RefreshTokenExpiration", "Your token expired");
}
