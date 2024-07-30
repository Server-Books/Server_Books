using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using System.Threading.Tasks;

namespace Server_Books.Controllers
{

    public class UserCreateController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserCreateController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("api/user/Create")]
        public async Task<ActionResult<string>> CreateUser([FromBody] Models.User user)
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al registrarse: {e.Message}");
            }
        }
    }
}