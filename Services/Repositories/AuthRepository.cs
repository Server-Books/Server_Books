using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Services.Interfaces;
using Server_Books.Models;
using Server_Books.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Server_Books.Services.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly IMailRepository _mailRepository;

        public AuthRepository(DataContext context, IOptions<JwtSettings> options, IMailRepository mailRepository)
        {
            _context = context;
            _jwtSettings = options.Value;
            _mailRepository = mailRepository;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // new Claim(ClaimTypes.Role, user.Rol)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.SecurityKey,
                audience: _jwtSettings.SecurityKey,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User Login(string email, string password)
{
    Console.WriteLine("AQUI---------------!!!!!");
    Console.WriteLine(email);

    if (string.IsNullOrEmpty(email))
    {
        return null; 
    }

    var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

    if (user != null && user.Password.ToLower() == password.ToLower())
    {
        // Enviar correo electrónico de inicio de sesión
        try
        {
            _mailRepository.SendEmailLogin(user.Email, "Login Successful", "You have successfully logged in.", user);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
        return user;
    }
    else
    {
        return null;
    }
}
        public void LogOutAsync()
        {
            throw new NotImplementedException();
        }
        // gSI=eFk4G3ZRy`(Kg£+<X(1VI4)5=RKw
        // 9Y9+JKvPyNR0qmUGeCT1oHfCwK2E4EK9YiUCRLXL9D8="
    }
}

