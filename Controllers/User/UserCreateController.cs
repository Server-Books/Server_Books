using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;

namespace Server_Books.Controllers
{
    [ApiController]
    [Route("api/Users/Create")]
    public class UserCreateController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserCreateController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        [HttpPost]
        [Route("api/Users/Create")]

        public async Task<ActionResult<Models.User>> CreateUser(Models.User user)
        {
            if (user == null)
            {
                return BadRequest("Los campos no pueden ser nulos");
            }
            try
            {
                await _userRepository.Create(user,user.Password);
                // await _mailerSendRepository.SendMailAsync(user.Email, user.Username, user.Id, user.Password);
                return Ok("Te has registrado correctamente!");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al registrarse: {e.Message}");
            }
        }
    }
}