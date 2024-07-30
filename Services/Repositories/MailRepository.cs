using Server_Books.Models;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Server_Books.Services
{
    public class MailRepository : IMailRepository
    {
        private readonly Email _emailSettings;

        public MailRepository(IOptions<Email> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmailCreateUser(string email, string subject, string body, User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") 
            { 
                Text = $"Hola, {user.Name},\n\n" +
                       $"Tu cuenta ha sido creada exitosamente.\n\n" +
                       $"Detalles de la cuenta:\n" +
                       $"Nombre: {user.Name}\n" +
                       $"Correo electrónico: {user.Email}\n" +
                       $"Contraseña: {user.Password}\n\n" +
                       $"Saludos,\n" +
                       $"{_emailSettings.SenderName}"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}



