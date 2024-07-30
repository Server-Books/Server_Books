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

        public BookLending GetById(int id)
        {
            return _context.BooksLending.Find(id);
        }

        public IEnumerable<BookLending> GetByLendingId(int lendingId)
        {
            return _context.BooksLending.Where(l => l.Id == lendingId).ToList();
        }

        public void Add(BookLending bookLending)
        {
            _context.BooksLending.Add(bookLending);
            _context.SaveChanges();
        }

        public void Update(BookLending bookLending)
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
    }
}
