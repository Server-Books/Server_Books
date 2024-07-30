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
        private readonly IBookRepository _bookRepository;

        public BookLendingController(IBookLendingRepository bookLendingRepository, IBookRepository bookRepository)
        {
            _bookLendingRepository = bookLendingRepository;

        }

        // Consultar todas las fechas de vencimiento para un libro específico
        [HttpGet("FechasVencimientoPorLibro/{bookId}")]
        public ActionResult GetLendingDueDatesByBook(int bookId)
        {
            var lendings = _bookLendingRepository.GetByLendingId(bookId);
            
            if (lendings == null || !lendings.Any())
            
            {
                return NotFound("No se encontraron préstamos para el libro especificado.");
            }

            var result = lendings.Select(l => new
            {
                l.BookId,
                l.DateOfReturn
            });

            return Ok(result);
        }
    }
}
