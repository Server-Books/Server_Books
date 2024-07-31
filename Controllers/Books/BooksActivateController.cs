using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.Books
{
    [ApiController]
    public class BooksActivateController : ControllerBase
    {
        public readonly IBookRepository _bookRepository;
        public BooksActivateController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        [HttpPatch]
        [Route("api/Books/Activate/{Id}")]
        public IActionResult ActivateBook(int Id){
            try{
                _bookRepository.ActivateBooK(Id);
                return Ok(new {message = "Libro Activado  con éxito"});
            }
            catch(Exception ex){
                return StatusCode(500, new {message = "Error al Activar libro: " + ex.Message});
            }
        } 

        
    }
}