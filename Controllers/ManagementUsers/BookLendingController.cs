using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using Server_Books.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server_Books.Controllers.ManagementUsers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookLendingController : ControllerBase
    {
        private readonly IBookLendingRepository _bookLendingRepository;

        public BookLendingController(IBookLendingRepository bookLendingRepository)
        {
            _bookLendingRepository = bookLendingRepository;
        }

        // Consultar fechas de vencimiento de un préstamo por ID
        [HttpGet("FechasVencimiento/{lendingId}")]
        public ActionResult GetLendingDueDate(int lendingId)
        {
            var lending = _bookLendingRepository.GetById(lendingId);
            if (lending == null)
            {
                return NotFound("El préstamo no existe.");
            }

            return Ok(new
            {
                lending.Id,
                lending.DateOfLoan,
                lending.DateOfReturn
            });
        }

        // Consultar todas las fechas de vencimiento para un libro específico
        [HttpGet("FechasVencimientoPorLibro/{bookId}")]
        public ActionResult GetLendingDueDatesByBook(int bookId)
        {
            var lendings = _bookLendingRepository.GetByBookId(bookId);
            if (lendings == null || !lendings.Any())
            {
                return NotFound("No se encontraron préstamos para el libro especificado.");
            }

            var result = lendings.Select(l => new
            {
                l.Id,
                l.DateOfLoan,
                l.DateOfReturn
            });

            return Ok(result);
        }
    }
}
