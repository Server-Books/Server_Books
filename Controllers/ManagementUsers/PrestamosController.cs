using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using Server_Books.Models;

namespace Server_Books.Controllers.ManagementUsers
{
    
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //Logica para traer todos los libros
        // GET: api/Books
        [Route("api/[controller]/VerLibros")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _bookRepository.GetAll();
            return Ok(books);
        }

        //Logica para realziar un prestamo de un libro
        [Route("api/[controller]/Prestamo")]
        [HttpPost]
        public ActionResult SolicitarPrestamo([FromBody] int bookId)
        {
            // Obtener el libro solicitado
            var libro = _bookRepository.GetById(bookId);
            if (libro == null)
            {
                return NotFound("El libro no existe.");
            }

            // Verificar si hay copias disponibles
            if (libro.CopiesAvailable <= 0)
            {
                return BadRequest("No hay copias disponibles para el libro solicitado.");
            }

            // Crear una solicitud de préstamo
            var prestamo = new BooksLending
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(15),
                Status = "Pendiente", 
                BookId = bookId,
                UserId = 1 
            };

            // Reducir el número de copias disponibles
            libro.CopiesAvailable--;
            if (libro.CopiesAvailable == 0)
            
            {
                libro.Status = "No Disponible";
            }

            // Guardar los cambios en el libro
            _bookRepository.Update(libro);

            // Agregar la solicitud de préstamo a la base de datos
            //_booksLendingRepository.Add(prestamo);

            // Retornar la respuesta
            return Ok(new
            {
                mensaje = "Solicitud de préstamo realizada con éxito. El libro está pendiente de aprobación.",
                prestamo
            });
        }
    }
}
