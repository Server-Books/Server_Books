using Microsoft.AspNetCore.Mvc;
using Server_Books.Services;
using System.Threading.Tasks;

namespace Server_Books.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCreateController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserCreateController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<string>> CreateUser([FromBody] Models.User user)
        {
            if (user == null)
            {
                return BadRequest("Los campos no pueden ser nulos");
            }
            try
            {
                await _userRepository.Create(user, user.Password);
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