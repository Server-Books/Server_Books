using Server_Books.Models;
using System.Collections.Generic;

namespace Server_Books.Services
{
    public interface IBookLendingRepository
    {
        BookLending GetById(int id);
        IEnumerable<BookLending> GetByLendingId(int lendingId);
        void Add(BookLending bookLending);
        void Update(BookLending bookLending);
        void Delete(int id);
    }
}

        // // Obtener todos los préstamos de libros
        // IEnumerable<BookLending> GetAllBookLendings();

        // // Obtener un préstamo de libro por ID
        // BookLending GetBookLendingById(int id);

        // // Agregar un nuevo préstamo de libro
        // void Add(BookLending bookLending);

        // // Actualizar un préstamo de libro existente
        // void Update(BookLending bookLending);

        // // Eliminar un préstamo de libro por ID
        // void Delete(int id);

