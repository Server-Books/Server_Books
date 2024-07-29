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
    public class ExportBooksByIdController : ControllerBase
    {
        // Inyeccion de la interfaz
        private readonly IExcelRepository _excelRepository;
        public ExportBooksByIdController(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }

        [HttpGet]
        [Route("loans/user/{userId}")]
        public async Task<IActionResult> ExportUserLoanHistoryAsync(int userId)
        {
            var stream = await _excelRepository.ExportBooksByIdAsync(userId);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"LoanHistory_User_{userId}.xlsx");
        }
    }
}