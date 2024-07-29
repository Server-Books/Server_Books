using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Services.Interfaces;
using Server_Books.Data;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;


namespace Server_Books.Services.Repositories
{
    public class ExcelRepository : IExcelRepository
    {
        // Inyección de la base del ManagementContext
        private readonly DataContext _context;
        public ExcelRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MemoryStream> ExportCustomersAsync()
        {
            var customers = await _context.Users.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Users");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Names";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Password";
                worksheet.Cells[1, 5].Value = "IdRole";
                // worksheet.Cells[1, 5].Value = "CreatedAt";
                // worksheet.Cells[1, 6].Value = "UpdatedAt";

                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];
                    worksheet.Cells[i + 2, 1].Value = customer.Id;
                    worksheet.Cells[i + 2, 2].Value = customer.Name;
                    worksheet.Cells[i + 2, 3].Value = customer.Email;
                    worksheet.Cells[i + 2, 4].Value = customer.Password;
                    worksheet.Cells[i + 2, 5].Value = customer.RoleId;
                    // worksheet.Cells[i + 2, 5].Value = customer.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                    // worksheet.Cells[i + 2, 6].Value = customer.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                }

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);
                stream.Position = 0;
                return stream;
            }
        }
    }
}