using Microsoft.EntityFrameworkCore;
using Server_Books.Models;
using Server_Books.Data;
using System.Collections.Generic;
using System.Linq;

namespace Server_Books.Services
{
    public class BookLendingRepository : IBookLendingRepository
    {
        private readonly DataContext _context;

        public BookLendingRepository(DataContext context)
        {
            _context = context;
        }

        public BookLending GetLendingById(int lendingId)
        {
            return _context.BooksLending.FirstOrDefault(l => l.Id == lendingId);
        }

        public void AddLendings(BookLending bookLending)
        {
            _context.BooksLending.Add(bookLending);
            _context.SaveChanges();
        }

        public void UpdateLendings(BookLending bookLending)
        {
            _context.BooksLending.Update(bookLending);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var lending = _context.BooksLending.Find(id);
            if (lending != null)
            {
                _context.BooksLending.Remove(lending);
                _context.SaveChanges();
            }
        }

        public void Loaned(BookLending bookLending)
        {
            _context.BooksLending.Add(bookLending);
            _context.SaveChanges();
        }
        public IEnumerable<BookLending> GetBookLendings()
        {
            return _context.BooksLending.ToList();
        }
    }
}
