using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Models;

namespace Server_Books.Services.Interfaces
{
    public interface IAuthRepository
    {
        User Login(string UserName, string Password);
        string GenerateToken(User User);
        void LogOutAsync();
        IEnumerable<User> GetAllBooks();
    }
}