using HR.LeaveManagement.Domain.Models;

namespace HR.LeaveManagement.Domain.Interfaces;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}
