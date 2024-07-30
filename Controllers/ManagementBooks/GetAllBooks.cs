using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.ManagementBooks
{
    private readonly IBookRepository _bookRepository;
    public GetAllBooks(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [ApiController]
    [Route("api/[controller]")]
    public class GetAllBooks : ControllerBase
    {
        #region Metodo listar todos los libros
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                var books = _bookRepository.GetAll();

                if (books == null || !books.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "No se encontraron libros.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Libros recuperados exitosamente.",
                    Data = books
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = "Ocurrió un error al obtener los libros.",
                    ErrorDetails = ex.Message
                });
            }
        }
        #endregion

    }
}