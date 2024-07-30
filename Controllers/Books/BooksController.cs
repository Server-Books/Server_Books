using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Models;
using ServerBooks.Services.Interfaces;

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
        [Route("api/Books")]
        public IActionResult BookAvailable(){
            try{
                var books =_booksRepository.GetAll();
                return Ok(books);
            }
            catch( Exception ex){
                return StatusCode(500, new {message = ex.Message});
            }
        }

    }
}