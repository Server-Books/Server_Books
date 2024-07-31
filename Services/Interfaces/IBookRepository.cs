using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Models;

namespace Server_Books.Services.Interfaces
{
    public interface IBookRepository
    {
        public void CreateBook (Book book);
        Book GetBookById (int Id);
        IEnumerable<Book> GetAllBooks(); 
        IEnumerable<Book> GetAllBooksAvailable();
        void UpdateBook(int id, Book book);
        public void DeleteBook(int Id);
        public void ActivateBooK(int Id);
        IEnumerable<Book> GetWaiting();
    }
}