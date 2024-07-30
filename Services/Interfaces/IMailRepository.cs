using Server_Books.Models;

namespace Server_Books.Services
{
    public interface IMailRepository
    {
        void SendEmailCreateUser(string email, string subject, string body, User user);
    }
}