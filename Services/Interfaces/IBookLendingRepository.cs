using Server_Books.Models;
using System.Collections.Generic;

namespace Server_Books.Services
{
    public interface IBookLendingRepository
    {
        BookLending GetById(int id);
        IEnumerable<BookLending> GetByBookId(int bookId);
        void Add(BookLending bookLending);
        void Update(BookLending bookLending);
        void Delete(int id);
    }
}
