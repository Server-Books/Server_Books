using Server_Books.Models;
using System.Collections.Generic;

namespace Server_Books.Services
{
    public interface IBookLendingRepository
    {
        BookLending GetLendingById(int lendingId);
        IEnumerable<BookLending> GetBookLendings();
        void AddLendings(BookLending bookLending);
        void UpdateLendings(BookLending bookLending);
        void Delete(int id);
        public void Loaned(BookLending bookLending);
    }
}

// // Obtener todos los préstamos de libros
// IEnumerable<BookLending> GetAllBookLendings();

// // Obtener un préstamo de libro por ID
// BookLending GetBookLendingById(int id);

// // Agregar un nuevo préstamo de libro
// void AddLendings(BookLending bookLending);

// // Actualizar un préstamo de libro existente
// void UpdateLendings(BookLending bookLending);

// // Eliminar un préstamo de libro por ID
// void Delete(int id);

