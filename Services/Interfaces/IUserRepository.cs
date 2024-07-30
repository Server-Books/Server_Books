using Server_Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server_Books.Services
{
    public interface IUserRepository
    {
        Task Create(User user,string password);

        Task<User> GetByIdAsync(int userId);
    }
}