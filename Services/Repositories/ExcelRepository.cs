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

        #region  Metodo para exportar todos los libros de la DB
        public async Task<MemoryStream> ExportAllBooksAsync()
        {
            var books = await _context.Books.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Books");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 3].Value = "Author";
                worksheet.Cells[1, 4].Value = "PublicationDate";
                worksheet.Cells[1, 5].Value = "Status";
                worksheet.Cells[1, 6].Value = "CopiesAvailable";
                // worksheet.Cells[1, 5].Value = "CreatedAt";
                // worksheet.Cells[1, 6].Value = "UpdatedAt";

                for (int i = 0; i < books.Count; i++)
                {
                    var book = books[i];
                    worksheet.Cells[i + 2, 1].Value = book.Id;
                    worksheet.Cells[i + 2, 2].Value = book.Title;
                    worksheet.Cells[i + 2, 3].Value = book.Author;
                    worksheet.Cells[i + 2, 4].Value = book.PublicationDate;
                    worksheet.Cells[i + 2, 5].Value = book.Status;
                    worksheet.Cells[i + 2, 6].Value = book.CopiesAvailable;
                    // worksheet.Cells[i + 2, 5].Value = book.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                    // worksheet.Cells[i + 2, 6].Value = book.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                }

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);
                stream.Position = 0;
                return stream;
            }
        }
        #endregion

        #region Metodo para exportar todos los libros segun el user
        public async Task<MemoryStream> ExportBooksByIdAsync(int userId)
        {
            var loans = await _context.BooksLending
                                    .Include(bl => bl.Book)
                                    .Where(bl => bl.UserId == userId)
                                    .ToListAsync();


            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("LoanHistory");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "StartDate";
                worksheet.Cells[1, 3].Value = "EndDate";
                worksheet.Cells[1, 4].Value = "Status";
                worksheet.Cells[1, 5].Value = "BookId";
                worksheet.Cells[1, 6].Value = "BookTitle";
                // worksheet.Cells[1, 5].Value = "CreatedAt";
                // worksheet.Cells[1, 6].Value = "UpdatedAt";

                for (int i = 0; i < loans.Count; i++)
                {
                    var loan = loans[i];
                    worksheet.Cells[i + 2, 1].Value = loan.Id;
                    worksheet.Cells[i + 2, 2].Value = loan.StartDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 3].Value = loan.EndDate?.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 4].Value = loan.Status;
                    worksheet.Cells[i + 2, 5].Value = loan.BookId;
                    worksheet.Cells[i + 2, 6].Value = loan.Book.Title;
                }

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);
                stream.Position = 0;
                return stream;
            }
        }
            #endregion

        #region Metodo para exportar todos los customer
        public async Task<MemoryStream> ExportCustomersAsync()
        {
            var books = await _context.Users.ToListAsync();

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

                for (int i = 0; i < books.Count; i++)
                {
                    var book = books[i];
                    worksheet.Cells[i + 2, 1].Value = book.Id;
                    worksheet.Cells[i + 2, 2].Value = book.Name;
                    worksheet.Cells[i + 2, 3].Value = book.Email;
                    worksheet.Cells[i + 2, 4].Value = book.Password;
                    worksheet.Cells[i + 2, 5].Value = book.RoleId;
                    // worksheet.Cells[i + 2, 5].Value = book.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                    // worksheet.Cells[i + 2, 6].Value = book.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                }

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);
                stream.Position = 0;
                return stream;
            }
        }
        #endregion
        }
    }