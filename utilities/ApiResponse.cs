using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Books.utilities
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string ErrorDetails { get; set; }
    }
}