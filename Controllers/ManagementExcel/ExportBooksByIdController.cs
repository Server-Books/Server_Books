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
    public class ExportBooksByIdController : ControllerBase
    {
        // Inyeccion de la interfaz
        private readonly IExcelRepository _excelRepository;
        public ExportBooksByIdController(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }
    }
}