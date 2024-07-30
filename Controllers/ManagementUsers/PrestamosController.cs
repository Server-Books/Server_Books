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

        // Consultar la disponibilidad de un libro por ID
        [HttpGet("Disponibilidad/{bookId}")]
        public ActionResult GetBookAvailability(int bookId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null)
            {
                return NotFound("El libro no existe.");
            }

            return Ok(new
            {
                book.Title,
                book.CopiesAvailable,
                Status = book.CopiesAvailable > 0 ? "Disponible" : "No Disponible"
            });
        }

        //Logica para realziar un prestamo de un libro
        [Route("api/[controller]/Prestamo")]
        [HttpPost]
        public ActionResult SolicitarPrestamo( int bookId, int UserId)
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
            var prestamo = new BookLending
            {
                DateOfLoan = DateOnly.FromDateTime(DateTime.Now),
                DateOfReturn = DateOnly.FromDateTime(DateTime.Now).AddDays(7),
                Status = "Pendiente", 
                BookId = bookId,
                UserId = UserId
            };

            _bookRepository.Update(libro);

            return Ok(new
            {
                mensaje = "Solicitud de préstamo realizada con éxito. El libro está pendiente de aprobación.",
                prestamo
            });
        }

        
    }
}
