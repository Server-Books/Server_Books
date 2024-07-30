using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Models;

namespace Server_Books.Services.Interfaces
{
    public interface IBookRepository
    {
        // Crear un nuevo libro
        void Create(Book book);

        // Obtener un libro por su ID
        Book GetById(int id);

        // Obtener todos los libros
        IEnumerable<Book> GetAll();

        // Actualizar un libro existente
        void Update(int id, Book book);

        // Eliminar un libro por su ID
        void Delete(int id);
    }
}