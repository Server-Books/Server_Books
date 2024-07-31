using Microsoft.AspNetCore.Mvc;
using Server_Books.Models;
using Server_Books.Services;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.ManagementUsers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AceptarPrestamoController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookLendingRepository _bookLendingRepository;

        public AceptarPrestamoController(IBookRepository bookRepository, IBookLendingRepository bookLendingRepository)
        {
            _bookRepository = bookRepository;
            _bookLendingRepository = bookLendingRepository;
        }

        [HttpPut("AceptarPrestamo")]
        public ActionResult AceptarPrestamo([FromBody] int lendingId)
        {
            var prestamo = _bookLendingRepository.GetLendingById(lendingId);
            if (prestamo == null)
            {
                return NotFound("El préstamo no existe.");
            }

            if (prestamo.Status != "Pending")
            {
                return BadRequest("El préstamo no está en estado pendiente.");
            }

            // Actualizar el estado del préstamo a 'Aprobado'
            prestamo.Status = "Approved";
            _bookLendingRepository.UpdateLendings(prestamo);

            // Obtener el libro asociado al préstamo
            var libro = _bookRepository.GetBookById(prestamo.BookId);
            if (libro == null)
            {
                return NotFound("El libro asociado no existe.");
            }

            // Reducir el número de copias disponibles
            libro.CopiesAvailable--;
            if (libro.CopiesAvailable == 0)
            {
                libro.Status = "NotAvailable";
            }
            else
            {
                libro.Status = "Loaned";
            }

            _bookRepository.UpdateBook(libro.Id, libro);

            return Ok(new
            {
                mensaje = "El préstamo ha sido aprobado con éxito.",
                prestamo
            });
        }
    }
}
