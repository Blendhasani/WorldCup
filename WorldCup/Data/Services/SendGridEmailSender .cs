using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;

namespace WorldCup.Data.Services
{
	public class SendGridEmailSender : IEmailSender
    {
	
    private readonly IConfiguration _configuration;

        public SendGridEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
            var msg = new SendGridMessage
            {
                From = new EmailAddress("noreply@example.com", "Example"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
