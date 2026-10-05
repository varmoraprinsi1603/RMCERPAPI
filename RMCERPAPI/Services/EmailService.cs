using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RMCERPAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(
            string toEmail,
            string subject,
            string body)
        {
            var fromName =
                _configuration["EmailSettings:FromName"];

            var fromEmail =
                _configuration["EmailSettings:FromEmail"];

            var smtpHost =
                _configuration["EmailSettings:SmtpHost"];

            var smtpPort =
                int.Parse(
                    _configuration["EmailSettings:SmtpPort"]
                );

            var username =
                _configuration["EmailSettings:Username"];

            var password =
                _configuration["EmailSettings:Password"];

            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(
                    fromEmail,
                    fromName
                );

                mail.To.Add(toEmail);

                mail.Subject = subject;

                mail.Body = body;

                mail.IsBodyHtml = false;

                using (var smtp = new SmtpClient(
                    smtpHost,
                    smtpPort))
                {
                    smtp.Credentials =
                        new NetworkCredential(
                            username,
                            password
                        );

                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}