using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using Server_Books.Models;
using Server_Books.utilities;

namespace Server_Books.Controllers.ManagementLoans
{
    [ApiController]
    [Route("api/loans")]
    public class BooksController : ControllerBase
    {
        private readonly IBookLendingRepository _bookLendingRepository;

        public BooksController(IBookLendingRepository bookLendingRepository)
        {
            _bookLendingRepository = bookLendingRepository;
        }

        #region Solicitar préstamo de un libro
        [Route("Loan")]
        [HttpPost]
        public ActionResult<ApiResponse> RequestLoanAsync([FromBody] int bookId)
        {
            try
            {
                // Obtener el libro solicitado
                var libro = _bookLendingRepository.GetById(bookId);
                if (libro == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "El libro no existe.",
                        Data = null
                    });
                }

                // Verificar si hay copias disponibles
                if (libro.CopiesAvailable <= 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "No hay copias disponibles para el libro solicitado.",
                        Data = null
                    });
                }

                // Crear una solicitud de préstamo
                var loan = new BookLending
                {
                    DateOfLoan = DateOnly.FromDateTime(DateTime.Now),
                    DateOfReturn = DateOnly.FromDateTime(DateTime.Now).AddDays(15),
                    Status = "Pendiente",
                    BookId = bookId,
                    UserId = 1 // Este valor debe ser dinámico basado en el usuario actual
                };

                // Reducir el número de copias disponibles
                libro.CopiesAvailable--;
                if (libro.CopiesAvailable == 0)
                {
                    libro.Status = "No Disponible";
                }

                // Guardar los cambios en el libro
                _bookLendingRepository.Update(libro);

                // Agregar la solicitud de préstamo a la base de datos
                _bookLendingRepository.Add(loan);

                // Retornar la respuesta
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Solicitud de préstamo realizada con éxito. El libro está pendiente de aprobación.",
                    Data = loan
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = "Ocurrió un error al procesar la solicitud de préstamo.",
                    ErrorDetails = ex.Message
                });
            }
        }
        #endregion
    }
}
