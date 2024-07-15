namespace HospitalManagementSystem.Application.Abstraction.Services;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}
