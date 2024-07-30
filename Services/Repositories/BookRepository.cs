using Microsoft.EntityFrameworkCore;
using Server_Books.Models;
using Server_Books.Data;
using Server_Books.Services.Interfaces;

namespace Server_Books.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public void Update(int id, Book book)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.CopiesAvailable = book.CopiesAvailable;

                _context.Books.Update(existingBook);
                _context.SaveChanges();
            }
        }
    }
}
