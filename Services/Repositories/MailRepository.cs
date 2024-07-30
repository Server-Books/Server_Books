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
                try
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error sending email", ex);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public void SendEmailLogin(string email, string subject, string body, User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") 
            { 
                Text = $"Hola, {user.Name},\n\n" +
                       $"Has iniciado sesión exitosamente.\n\n" +
                       $"Saludos,\n" +
                       $"{_emailSettings.SenderName}"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error sending email", ex);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public void SendEmailLoanRequest(string email, string subject, string body, BookLending prestamo)
{
    var emailMessage = new MimeMessage();
    emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
    emailMessage.To.Add(new MailboxAddress("", email));
    emailMessage.Subject = subject;
    emailMessage.Body = new TextPart("plain") 
    { 
        Text = $"Hola,\n\n" +
               $"Tu solicitud de préstamo ha sido recibida.\n\n" +
               $"Detalles del préstamo:\n" +
               $"Libro ID: {prestamo.BookId}\n" +
               $"Fecha de inicio: {prestamo.DateOfLoan}\n" +
               $"Fecha de fin: {prestamo.DateOfReturn}\n" +
               $"Estado: {prestamo.Status}\n\n" +
               $"Saludos,\n" +
               $"{_emailSettings.SenderName}"
    };

    using (var client = new MailKit.Net.Smtp.SmtpClient())
    {
        try
        {
            client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
            client.Authenticate(_emailSettings.Username, _emailSettings.Password);
            client.Send(emailMessage);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error sending email", ex);
        }
        finally
        {
            client.Disconnect(true);
        }
    }
}

        public void SendEmailLoanApproved(string email, string subject, string body, User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") 
            { 
                Text = $"Hola, {user.Name},\n\n" +
                       $"Tu solicitud de préstamo ha sido aprobada.\n\n" +
                       $"Saludos,\n" +
                       $"{_emailSettings.SenderName}"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error sending email", ex);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public void SendEmailDueDateReminder(string email, string subject, string body, User user, DateTime dueDate)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") 
            { 
                Text = $"Hola, {user.Name},\n\n" +
                       $"Este es un recordatorio de que tu préstamo vence el {dueDate.ToString("dd/MM/yyyy")}.\n\n" +
                       $"Saludos,\n" +
                       $"{_emailSettings.SenderName}"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error sending email", ex);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public void SendEmailLoanRequestAdmin(string email, string subject, string body, User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") 
            { 
                Text = $"Hola,\n\n" +
                       $"El usuario {user.Name} ha realizado una solicitud de préstamo para su revisión y autorización.\n\n" +
                       $"Saludos,\n" +
                       $"{_emailSettings.SenderName}"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error sending email", ex);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}