using System.Transactions;

namespace ProyectoASPNETGRUPOC.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string toEmail, string subject, string body);
    }
}
