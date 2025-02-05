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
    public class BooksUpdateController : ControllerBase
    {
        public readonly IBooksRepository _booksRepository;
        public BooksUpdateController(IBooksRepository booksRepository){
            _booksRepository = booksRepository;
        }

        [HttpPut]
        [Route("api/Books/{Id}")]
        public IActionResult ActualizaLibro(int Id,  Book book)
        {   
            try
            {
                _booksRepository.UpdateBook(Id, book);
                return Ok(new{ message = "Se ha actualizado con exito"});
            }
            catch (Exception ex)
            {
                return BadRequest(new{ message = "Error al actualizar libro "+ ex.Message });
            }
        } 
    }
}