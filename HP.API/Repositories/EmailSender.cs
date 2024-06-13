using System.Net;
using System.Net.Mail;

namespace HP.API.Repositories
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "happypet.test@outlook.com"; // Your Outlook email address
            var pw = "zdravozdravo123"; // Your Outlook email password

            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message));
        }
    }
}
