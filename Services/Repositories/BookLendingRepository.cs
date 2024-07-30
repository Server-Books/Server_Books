using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Data;
using Server_Books.Models;
using Server_Books.Services.Interfaces;


namespace Server_Books.Services.Repositories
{
    public class BookLendingRepository : IBookLendingRepository
    {
        public readonly DataContext _context;
        public BookLendingRepository(DataContext context)
        {
            _context = context;
        }
    }
}