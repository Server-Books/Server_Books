using Microsoft.EntityFrameworkCore;


namespace SeverBooks.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            // Implementamos los modelos de la DB
            
        }


    }
}