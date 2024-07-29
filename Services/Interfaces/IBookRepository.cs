using System.Collections.Generic;
using System.Threading.Tasks;
using Server_Books.Models;

namespace Server_Books.Services
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
