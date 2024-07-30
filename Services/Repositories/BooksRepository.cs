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
        return  _context.Books.Where(x => x.Status == "Available").ToList();
        }

        public Book GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(int Id, Book book)
        {
            book.Id = Id;
            
            _context.Books.Update(book);
            _context.SaveChanges();
            
        }
    }
}