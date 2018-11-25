using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userforRegisterDTO)
        {
            userforRegisterDTO.Username = userforRegisterDTO.Username.ToLower();

            if (await _repo.UserExists(userforRegisterDTO.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = userforRegisterDTO.Username
            };

            var createdUser = await _repo.Register(userToCreate, userforRegisterDTO.Password);

            return StatusCode(201);
        }
    }
}