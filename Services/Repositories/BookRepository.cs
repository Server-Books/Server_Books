using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Data;
using Server_Books.Models;
using Server_Books.Services.Interfaces;

namespace Server_Books.Services.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateBook(Book book)
        {
            book.Status = "Available";
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
            // return _context.Books.Where(x => x.Status == "Available").ToList();
        }

        public IEnumerable<Book> GetAllBooksAvailable()
        {
            return _context.Books.Where(x => x.Status == "Available").ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public void UpdateBook(int id, Book book)
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
            else
            {
                throw new ArgumentException($"No se encontró el libro con el ID {id}");
            }
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                if (book.Status == "Loaned")
                {
                    throw new InvalidOperationException("No se puede eliminar un libro que está prestado.");
                }

                book.Status = "NotAvailable"; 
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("El libro no fue encontrado.");
            }
        }

        public void ActivateBooK(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                book.Status = "Available";
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("El libro no fue encontrado.");
            }
        }

        public IEnumerable<Book> GetWaiting()
        {
            return _context.Books.Where(x => x.Status == "Available").ToList();
        }
    }
}
