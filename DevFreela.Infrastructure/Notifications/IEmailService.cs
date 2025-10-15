using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DevFreela.Infrastructure.Notifications
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _client;
        private readonly string _fromEmail;
        private readonly string _fromName;
        public EmailService(ISendGridClient client, IConfiguration configuration)
        {
            _client = client;
            _fromEmail = configuration.GetValue<string>("SendGrid:FromEmail") ?? "";
            _fromName = configuration.GetValue<string>("SendGrid:FromName" ) ?? "";
        }
        public async Task SendAsync(string email, string subject, string message)
        {
            try
            {
                var sendGridMessage = new SendGridMessage
                {
                    From = new EmailAddress(_fromEmail, _fromName),
                    Subject = subject
                };

                sendGridMessage.AddContent(MimeType.Text, message);
                sendGridMessage.AddTo(new EmailAddress(email));

                var response = await _client.SendEmailAsync(sendGridMessage);

                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Body.ReadAsStringAsync();
                    throw new Exception($"SendGrid error: {response.StatusCode} - {body}");
                }
            }
            catch (Exception ex)
            {
                // Aqui você descobre se o erro é SendGridKey inválida, endpoint incorreto, etc.
                throw new Exception($"Falha ao enviar email: {ex.Message}", ex);
            }
        }
    }
}

