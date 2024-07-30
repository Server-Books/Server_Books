using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using Server_Books.Models;
using ServerBooks.Services.Interfaces;

namespace Server_Books.Controllers.ManagementUsers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookLendingRepository _bookLendingRepository;

        public BooksController(IBookRepository bookRepository, IBookLendingRepository bookLendingRepository)
        {
            _bookRepository = bookRepository;
            _bookLendingRepository = bookLendingRepository;
        }

        // Lógica para traer todos los libros
        // GET: api/Books/VerLibros
        [HttpGet("VerLibros")]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _bookRepository.GetAll();
            return Ok(books);
        }

        [HttpGet("VerPrestamos")]
        public ActionResult<IEnumerable<BookLending>> lookAvailible()
        {
            var lends = _bookLendingRepository.GetBookLendings();
            return Ok(lends);
        }



        // Lógica para realizar un préstamo de un libro
        // POST: api/Books/Prestamo
        [HttpPost("Prestamo")]
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
            var prestamo = new BookLending
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(15),
                Status = "Pending", 
                BookId = bookId,
                UserId = 1 
            };

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