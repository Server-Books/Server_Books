using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Books.Services;
using Server_Books.Models;

namespace Server_Books.Controllers.ManagementUsers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IBookLoanRepository _bookLoanRepository;

        public PrestamosController(IBookLoanRepository loanRepository)
        {
            _bookLoanRepository = loanRepository;
        }

        // Logica para realizar un prestamo de un libro
        [Route("SolicitarPrestamo")]
        [HttpPost]
        public ActionResult SolicitarPrestamo([FromBody] BookLending solicitud)
        {
            try
            {
                // Llamar al método del repositorio para solicitar el préstamo
var prestamo = _bookLoanRepository.SolicitarPrestamo(solicitud.UserId, solicitud.BookId);
                // Retornar la respuesta
                return Ok(new
                {
                    mensaje = "Solicitud de préstamo realizada con éxito. El libro está pendiente de aprobación.",
                    prestamo
                });
            }
            catch (DbUpdateException dbEx)
            {
                // Capturar la excepción interna
                return BadRequest(new
                {
                    mensaje = "Error al guardar los cambios en la base de datos.",
                    detalle = dbEx.InnerException?.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Logica para buscar un libro por ID
        [Route("BuscarLibroPorId")]
        [HttpGet]
        public ActionResult<Book> BuscarLibroPorId([FromQuery] int bookId)
        {
            try
            {
                var libro = _bookLoanRepository.BuscarLibroPorId(bookId);
                if (libro == null)
                {
                    return NotFound("Libro no encontrado.");
                }
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Logica para buscar un libro por nombre
        [Route("BuscarLibroPorNombre")]
        [HttpGet]
        public ActionResult<Book> BuscarLibroPorNombre([FromQuery] string nombre)
        {
            try
            {
                var libro = _bookLoanRepository.BuscarLibroPorNombre(nombre);
                if (libro == null)
                {
                    return NotFound("Libro no encontrado.");
                }
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}