using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.ManagementExcel
{
    [ApiController]
    [Route("api/export/")]
    public class ExportCustomersController : ControllerBase
    {
        // Inyeccion de la interfaz
        private readonly IExcelRepository _excelRepository;
        public ExportCustomersController(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }

        // Metodo para exportar los customers
        [HttpGet]
        [Route("customers")]
        public async Task<IActionResult> ExportCustomersAsync()
        {
            var stream = await _excelRepository.ExportCustomersAsync();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
        }
    }
}