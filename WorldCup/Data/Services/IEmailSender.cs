using System.Net.Mail;

namespace WorldCup.Data.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}