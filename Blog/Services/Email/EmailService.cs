using System.Net;
using System.Net.Mail;
using Blog.Configuration;
using Microsoft.Extensions.Options;

namespace Blog.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;
        private readonly SmtpClient _client;

        public EmailService(IOptions<SmtpSettings> options)
        {
            _settings = options.Value;
            _client = new SmtpClient(_settings.Server)
            {
                Credentials = new NetworkCredential(_settings.UserName, _settings.Passsword)
            };
        }

        public Task SendEmail(string email, string subject, string message)
        {
            var mail = new MailMessage(
                "body@test.net",
                email,
                subject,
                message
                );
            return _client.SendMailAsync(mail);
        }
    }
}
