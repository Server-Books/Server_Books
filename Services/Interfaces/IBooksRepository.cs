using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Models;

namespace ServerBooks.Services.Interfaces
{
    public interface IBooksRepository
    {
        public void CreateBook (Book book);
        Book GetById (int Id);
        IEnumerable<Book> GetAll ();
        public void UpdateBook(int Id, Book book);
    }
}