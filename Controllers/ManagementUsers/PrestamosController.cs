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
        private readonly IMailRepository _mailRepository;
        private readonly IUserRepository _UserRepository;

        public BooksController(IBookRepository bookRepository, IBookLendingRepository bookLendingRepository, IMailRepository mailRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _UserRepository = userRepository;
            _bookLendingRepository = bookLendingRepository;
            _mailRepository = mailRepository;
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
public async Task<ActionResult> SolicitarPrestamo([FromBody] int bookId)
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
        DateOfReturn = DateOnly.FromDateTime(DateTime.Now).AddDays(15),
        Status = "Pending", 
        BookId = bookId,
        UserId = 1
    };

    // Agregar la solicitud de préstamo a la base de datos
    _bookLendingRepository.Loaned(prestamo);

  
    return Ok(new
    {
        mensaje = "Solicitud de préstamo realizada con éxito. El libro está pendiente de aprobación.",
        prestamo
    });
}

    }
}

