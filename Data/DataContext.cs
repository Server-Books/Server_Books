using Microsoft.EntityFrameworkCore;
using Server_Books.Models;


namespace Server_Books.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set;}
        public DbSet<Rol> Roles {get; set;}
        public DbSet<Book> Books { get; set;}
        public DbSet<BooksLending> BooksLendings { get; set;}
        public DbSet<GendersBook> GendersBooks {get; set;}
        public DbSet<Gender> Genders {get; set;}

        
    }
}