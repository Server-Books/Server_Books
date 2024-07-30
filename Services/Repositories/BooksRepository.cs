using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Data;
using Server_Books.Models;
using Server_Books.Services.Interfaces;


namespace Server_Books.Services.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        public readonly DataContext _context;
        public BooksRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateBook(Book book)
        {
            book.Status = "Available";
            _context.Books.Add(book);
            _context.SaveChanges();

        }

        public IEnumerable<Book> GetAll()
        {
        return  _context.Books.Where(x => x.Status == "Available").ToList();
        }

        public Book GetById(int Id)
        {
            var BookFind = _context.Books.Find(Id);
            return BookFind;
        }

        public void UpdateBook(int Id, Book book)
        {
            book.Id = Id;
            
            _context.Books.Update(book);
            _context.SaveChanges();
            
        }
        public void DeleteBook(int Id){
            var book = _context.Books.Find(Id);
            if (book != null)
            {
                
                if (book.Status == "Loaned") {
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

        public void ActivateBooK(int Id){
            var book = _context.Books.Find(Id);
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
        return  _context.Books.Where(x => x.Status == "Available").ToList();
        }

    }
}