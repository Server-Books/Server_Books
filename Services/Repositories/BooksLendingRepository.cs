
using Server_Books.Data;
using Server_Books.Models;
using ServerBooks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ServerBooks.Services.Repositories
{
    public class BookLendingRepository : IBookLendingRepository
    {
        private readonly DataContext _context;

        public BookLendingRepository(DataContext context)
        {
            _context = context;
        }

        public BookLending GetById(int id)
        {
            return _context.BooksLending.FirstOrDefault(bl => bl.Id == id);
        }

        public void Update(BookLending bookLending)
        {
            _context.BooksLending.Update(bookLending);
            _context.SaveChanges();
        }

        public void Loaned(BookLending bookLending){
            _context.BooksLending.Add(bookLending);
            _context.SaveChanges();
        }
        public IEnumerable<BookLending> GetBookLendings()
        {
        return  _context.BooksLending.Include(x => x.Book).Include(n =>n.User).ToList();
        }

    }
}
