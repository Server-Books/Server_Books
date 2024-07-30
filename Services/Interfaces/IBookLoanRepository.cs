using Server_Books.Models;

namespace Server_Books.Services
{
    public interface IBookLoanRepository
    {
        BookLending SolicitarPrestamo(int userId, int bookId);
        Book BuscarLibroPorId(int bookId);
        Book BuscarLibroPorNombre(string nombre);
    }
}