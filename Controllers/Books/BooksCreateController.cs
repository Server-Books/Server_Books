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
    public class BooksCreateController : ControllerBase
    {
        public readonly IBooksRepository _booksRepository;
        public BooksCreateController(IBooksRepository booksRepository){
            _booksRepository = booksRepository;
        }

        [HttpPost]
        [Route("api/Books")]
        public IActionResult Createbook([FromBody]Book book){
            try{
                _booksRepository.CreateBook(book);
                return StatusCode(200, new {message = "libro creado con éxito"});
            }
            catch( Exception ex){
                return StatusCode(500, new {message = ex.Message});
            }
        }

    }
}