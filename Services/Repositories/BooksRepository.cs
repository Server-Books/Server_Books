using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Data;
using Server_Books.Models;
using ServerBooks.Services.Interfaces;


namespace ServerBooks.Services.Repositories
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
            throw new NotImplementedException();
        }

        public Book GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(int Id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}