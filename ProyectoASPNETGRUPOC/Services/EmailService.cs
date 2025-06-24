using Microsoft.Extensions.Options;
using ProyectoASPNETGRUPOC.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ProyectoASPNETGRUPOC.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(string toEmail, string subject, string body)
        {
			try
			{
				var smtpClient = new SmtpClient(_config["EmailSettings:SmtpServer"])
				{
					Port = int.Parse(_config["EmailSettings:SmtpPort"]),
					Credentials = new NetworkCredential(_config["EmailSettings:Username"], _config["EmailSettings:Password"]),
					EnableSsl = true
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress(_config["EmailSettings:SenderEmail"], _config["EmailSettings:SenderName"]),
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				};

				mailMessage.To.Add(toEmail);
				await smtpClient.SendMailAsync(mailMessage);
			}
			catch (Exception ex)
			{
				throw new Exception("Error al enviar el email.");
			}
        }
    }
    }
