using Server_Books.Models;

namespace Server_Books.Services
{
    public interface IMailRepository
    {
        void SendEmailCreateUser(string email, string subject, string body, User user);
        void SendEmailLogin(string email, string subject, string body, User user);
        void SendEmailLoanRequest(string email, string subject, string body, BookLending prestamo);
        void SendEmailLoanApproved(string email, string subject, string body, User user);
        void SendEmailDueDateReminder(string email, string subject, string body, User user, DateTime dueDate);
        void SendEmailLoanRequestAdmin(string email, string subject, string body, User user);
    }
}