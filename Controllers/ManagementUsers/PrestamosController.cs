using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using Server_Books.Models;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.ManagementUsers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookLendingRepository _bookLendingRepository;

        public PrestamosController(IBookRepository bookRepository, IBookLendingRepository bookLendingRepository)
        {
            _bookRepository = bookRepository;
            _bookLendingRepository = bookLendingRepository;
        }

        // Lógica para traer todos los libros
        // GET: api/Books/VerLibros
        [HttpGet("TodosLosLibros")]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            return Ok(books);
        }

        // Tarer los libros dosponibles
        [HttpGet("LibrosDisponibles")]
        public ActionResult<IEnumerable<Book>> GetAllBooksAvailable()
        {
            var books = _bookRepository.GetAllBooksAvailable();
            return Ok(books);
        }

        // Consultar la disponibilidad de un libro por ID
        [HttpGet("Disponibilidad/{bookId}")]
        public ActionResult GetBookAvailability(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
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

        [HttpGet("TodosLosPrestamos")]
        public ActionResult<IEnumerable<BookLending>> GetBookLendings()
        {
            var lends = _bookLendingRepository.GetBookLendings();
            return Ok(lends);
        }



        // Lógica para realizar un préstamo de un libro
        // POST: api/Books/Prestamo
        [HttpPost("Prestamo")]
        public ActionResult SolicitarPrestamo(int bookId, int userId)
        {
            // Obtener el libro solicitado
            var libro = _bookRepository.GetBookById(bookId);
            if (libro == null)
            {
                return NotFound("El libro no existe.");
            }

            // Verificar si hay copias disponibles
            if (libro.CopiesAvailable <= 0)
            {
                return BadRequest("No hay copias disponibles para el libro solicitado.");
            }

            // // Reducir el número de copias disponibles
            // libro.CopiesAvailable--;

            // Crear una solicitud de préstamo
            
            var prestamo = new BookLending
            {
                DateOfLoan = DateOnly.FromDateTime(DateTime.Now),
                DateOfReturn = DateOnly.FromDateTime(DateTime.Now).AddDays(7),
                Status = "Pending",
                BookId = bookId,
                UserId = userId
            };

            // // Actualizar el libro en el repositorio
            // _bookRepository.UpdateBook(libro);

            // Agregar la solicitud de préstamo a la base de datos
            _bookLendingRepository.Loaned(prestamo);

            // Retornar la respuesta
            return Ok(new
            {
                mensaje = "Solicitud de préstamo realizada con éxito. El libro está pendiente de aprobación.",
                prestamo
            });
        }
    }
}