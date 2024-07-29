using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Books.Services.Interfaces
{
    public interface IExcelRepository
    {
        Task<MemoryStream> ExportCustomersAsync();
        // Task<MemoryStream> ExportBooksByIdAsync();
        Task<MemoryStream> ExportAllBooksAsync();
    }
}