using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Data;
using Server_Books.Models;
using EntityFrameworkCoreJwtTokenAuth.Models.Auth;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Server_Books.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMailRepository _mailRepository;

        public UserRepository(DataContext context, IMailRepository mailRepository)
        {
            _context = context;
            _mailRepository = mailRepository;
        }

        public async Task Create(Models.User user, string password)
        {
            var email = user.Email;
            var emailer = await _context.Users.AnyAsync(u => u.Email == email);
            if (string.IsNullOrWhiteSpace(email) || emailer.Equals(email))
                throw new ArgumentException("Email es requerido o ya esta utilizado");

            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
                throw new ArgumentException("Contraseña Insegura");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Enviar correo al usuario recién creado
            _mailRepository.SendEmailCreateUser(user.Email, "Bienvenido a Server Books", "Tu cuenta ha sido creada exitosamente.", user);
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsLower)) return false;
            if (!password.Any(char.IsDigit)) return false;
            if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return false;

            return true;
        }
    }
}