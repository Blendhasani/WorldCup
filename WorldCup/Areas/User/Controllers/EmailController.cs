using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WorldCup.Controllers
{
    [Area("User")]
    public class EmailController : Controller
    {
        [Route("User/Email/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("User/Email/Index")]
        [HttpPost]
        public async Task<IActionResult> Index(string recipient, string subject, string message)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("mygganbu@gmail.com");
                    mail.To.Add(recipient);
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("saraqialbin@gmail.com", "feiippdqwspvojia");
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }

                ViewBag.StatusCode = System.Net.HttpStatusCode.Accepted;
            }
            catch
            {
                ViewBag.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return View();
        }
    }


}