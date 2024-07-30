using Microsoft.EntityFrameworkCore;
using Server_Books.Data;
using Server_Books.Models;
using System.Threading.Tasks;

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

        public async Task Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email es requerido");

            var emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (emailExists)
                throw new ArgumentException("Email ya está en uso");

            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
                throw new ArgumentException("Contraseña insegura");

            // Asignar la contraseña directamente
            user.Password = password;

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