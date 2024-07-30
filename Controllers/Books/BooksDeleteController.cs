using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.Books
{
    [ApiController]
    public class BooksDeleteController : ControllerBase
    {
        public readonly IBooksRepository _bookRepository;
        public BooksDeleteController(IBooksRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        [HttpPatch]
        [Route("api/Books/Delete/{Id}")]
        public IActionResult DeleteBook(int Id){
            try{
                _bookRepository.DeleteBook(Id);
                return Ok(new {message = "Libro eliminado con éxito"});
            }
            catch(Exception ex){
                return StatusCode(500, new {message = "Error al eliminar libro: " + ex.Message});
            }
        } 

        
    }
}