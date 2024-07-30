using Microsoft.EntityFrameworkCore;
using Server_Books.Models;
using Server_Books.Data;
using System.Linq;

namespace Server_Books.Services
{
    public class BookLoanRepository : IBookLoanRepository
    {
        private readonly DataContext _context;

        public BookLoanRepository(DataContext context)
        {
            _context = context;
        }

        public BookLending SolicitarPrestamo(int userId, int bookId)
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var endDate = startDate.AddDays(15);

            var prestamo = new BookLending
            {
                UserId = userId,
                BookId = bookId,
                StartDate = startDate,
                EndDate = endDate,
                Status = "Pending"
            };

            _context.BooksLending.Add(prestamo);
            _context.SaveChanges();

            return prestamo;
        }

        public Book BuscarLibroPorId(int bookId)
        {
            return _context.Books.Find(bookId);
        }

        public Book BuscarLibroPorNombre(string nombre)
        {
            return _context.Books.FirstOrDefault(b => b.Title == nombre);
        }
    }
}