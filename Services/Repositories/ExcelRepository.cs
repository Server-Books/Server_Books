using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Services.Interfaces;

namespace Server_Books.Services.Repositories
{
    public class ExcelRepository : IExcel
    {
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
            throw new NotImplementedException();
        }
    }
}