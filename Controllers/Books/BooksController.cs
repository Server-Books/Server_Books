using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Models;
using Server_Books.Services.Interfaces;
using System.Security.Claims;

namespace Server_Books.Controllers.Books
{   
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly IBooksRepository _booksRepository;
        public BooksController(IBooksRepository booksRepository){
            _booksRepository = booksRepository;
        }

        [HttpGet]
        [Route("api/Books/Availible")]
        public IActionResult BookAvailable(){
            try{
                var books =_booksRepository.GetAll();
                return Ok(books);
            }
            catch( Exception ex){
                return StatusCode(500, new {message = ex.Message});
            }
        }



        [HttpGet]
        [Route("api/Books/{Id}")]
        public IActionResult GetBook(int Id){
            try{
                var book = _booksRepository.GetById(Id);
                if(book == null){
                    return NotFound(new {message = "Libro no encontrado"});
                }
                return Ok(book);
            }
            catch( Exception ex){
                return StatusCode(500, new {message = ex.Message});
            }
        }


private int GetAuthenticatedUserId()
{
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
    {
        throw new InvalidOperationException("No se pudo encontrar el ID del usuario autenticado.");
    }

    return int.Parse(userIdClaim.Value);
}


    }
}


