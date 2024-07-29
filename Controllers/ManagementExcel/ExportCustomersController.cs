using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Services.Interfaces;

namespace Server_Books.Controllers.ManagementExcel
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportCustomersController : ControllerBase
    {
        // Inyeccion de la interfaz
        private readonly IExcel _excel;
        public ExportCustomersController(IExcel excel)
        {
            _excel = excel;
        }

        // Metodo para exportar los customers
        [HttpGet]
        [Route("api/customers/export")]
        public async Task<IActionResult> ExportCustomersAsync()
        {
            var stream = await _excel.ExportCustomersAsync();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
        }
    }
}