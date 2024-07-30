using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server_Books.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Models;

namespace Server_Books.Controllers.Auth
{
    public class AuthLogOutController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthLogOutController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        [Authorize]
        [Route("api/logout")]

        public async  Task<IActionResult> LogOutUser ()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            return Ok(new {message = "Se ha cerrado sesión correctamente"});
        }
    
    }
}