using Microsoft.EntityFrameworkCore;
using Server_Books.Models;


namespace Server_Books.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // Implementamos los modelos de la DB
        public DbSet<User> Users { get; set;}
        public DbSet<Role> Roles {get; set;}
        public DbSet<Book> Books { get; set;}
        public DbSet<BooksLending> BookLending { get; set;}
        public DbSet<GenderBook> GendersBooks {get; set;}
        public DbSet<Gender> Genders {get; set;}
    }
}