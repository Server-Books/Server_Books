using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Services.Interfaces;
using SeverBooks.Data;

namespace Server_Books.Services.Repositories
{
    public class ExcelRepository : IExcel
    {
        // Inyección de la base del ManagementContext
        private readonly DataContext _context;
        public ExcelRepository(DataContext context)
        {
            _context = context;
        }

        public Task<MemoryStream> ExportAllBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MemoryStream> ExportBooksByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MemoryStream> ExportCustomersAsync()
        {
            var customers = _context.
        }
    }
}