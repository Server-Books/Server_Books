using Server_Books.Models;
namespace ServerBooks.Services.Interfaces
{
    public interface IBookLendingRepository
    {
        BookLending GetById(int id);
        void Update(BookLending bookLending);

        public void Loaned(BookLending bookLending);

        public IEnumerable<BookLending> GetBookLendings();
        
    }
}