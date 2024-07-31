using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Models;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.Books
{   
    [ApiController]
    [Route("api/Books/")]
    public class BooksController : ControllerBase
    {
        public readonly IBookRepository _booksRepository;
        public BooksController(IBookRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        [Route("Availible")]
        public IActionResult BookAvailable(){
            try{
                var books =_booksRepository.GetAllBooks();
                return Ok(books);
            }
            catch( Exception ex){
                return StatusCode(500, new {message = ex.Message});
            }
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetBook(int Id){
            try{
                var book = _booksRepository.GetBookById(Id);
                if(book == null){
                    return NotFound(new {message = "Libro no encontrado"});
                }
                return Ok(book);
            }
            catch( Exception ex){
                return StatusCode(500, new {message = ex.Message});
            }
        }


    }
}