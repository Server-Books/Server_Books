using Server_Books.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server_Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Server_Books.Data;

namespace Server_Books.Controllers.Auth
{
    
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        [Route("api/auth/login")]
        public IActionResult LoginUser([FromBody] UserCred usercred)
        {
            try 
            {
                var user = _authRepository.Login(usercred.Email, usercred.Password);
                if (user == null)
                {   
                    //.
                    return Unauthorized("Credenciales invalidas");
                }

                var tokenString = _authRepository.GenerateToken(user);
                return Ok(new { token = tokenString, userId = user.Id, userName = user.Name, email = user.Email });
            }
            catch (Exception e)
            {
                return BadRequest("Error al ingresar"+e);
            }
        }
    }
}