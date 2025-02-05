using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.ManagementExcel
{
    [ApiController]
    [Route("api/export")]
    public class ExportAllBooksController : ControllerBase
    {
        // Inyeccion de la interfaz
        private readonly IExcelRepository _excelRepository;
        public ExportAllBooksController(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }

        // Metodo para exportar los books
        [HttpGet]
        [Route("/books")]
        public async Task<IActionResult> ExportAllBooksAsync()
        {
            var stream = await _excelRepository.ExportAllBooksAsync();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Books.xlsx");
        }
    }
}