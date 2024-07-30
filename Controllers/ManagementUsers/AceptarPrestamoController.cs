using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using Server_Books.Models;
using ServerBooks.Services.Interfaces;

namespace Server_Books.Controllers.ManagementUsers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookLendingController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookLendingRepository _bookLendingRepository;

        public BookLendingController(IBookRepository bookRepository, IBookLendingRepository bookLendingRepository)
        {
            _bookRepository = bookRepository;
            _bookLendingRepository = bookLendingRepository;
        }

        // Lógica para aceptar un préstamo pendiente
        // PUT: api/BookLending/AceptarPrestamo
        [HttpPut("AceptarPrestamo")]
        public ActionResult AceptarPrestamo([FromBody] int lendingId)
        {
            // Obtener el préstamo pendiente
            var prestamo = _bookLendingRepository.GetById(lendingId);
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
            _bookLendingRepository.Update(prestamo);

            // Obtener el libro asociado al préstamo
            var libro = _bookRepository.GetById(prestamo.BookId);
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


            _bookRepository.Update(libro);

            
            return Ok(new
            {
                mensaje = "El préstamo ha sido aprobado con éxito.",
                prestamo
            });
        }
    }
}