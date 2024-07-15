namespace HospitalManagementSystem.Application.Email;

public class EmailSettings
{
    public string LoginEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
}
