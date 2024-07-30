using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Data;
using Server_Books.Models;
using EntityFrameworkCoreJwtTokenAuth.Models.Auth;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Server_Books.Services;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task Create(Models.User user,string password,string email)
    {
        // var emailer =  await _context.Users.AnyAsync(u => u.Email == email);
        if (string.IsNullOrWhiteSpace(email) || await _context.Users.AnyAsync(u => u.Email == email))
             throw new ArgumentException("Email es requerido o ya esta utilizado");

         if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
         throw new ArgumentException("Contraseña Insegura");
        
      
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
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
