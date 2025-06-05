using Bookliz_Backend.Models;
using Bookliz_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookliz_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLogin)
        {
            if (userLogin == null)
                return BadRequest("Invalid login data.");

            var result = await _authService.LoginAsync(userLogin);
            // Check if the result is valid
            if (result == null)
                return Unauthorized("Invalid username or password.");
            if (result == "username")
                return Unauthorized("Username not found.");
            if (result == "password")
                return Unauthorized("Incorrect password.");
            if (result == string.Empty)
                return Unauthorized("Invalid username or password.");
            if (result == "role")
                return Unauthorized("Invalid User Role.");
                
            
            // Return the result if login is successful
            return Ok(new { Token = result, Message = "Login successful."});
        }

        [HttpPost("logout")]
        public async Task<bool> Logout()
        {
            var result = await _authService.LogoutAsync();
            return result;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel userdto)
        {
            if (userdto == null)
                return BadRequest("Invalid student data.");

            var result = await _authService.RegisterAsync(userdto);
            if (result == null)
                return BadRequest("Registration failed.");

            return Ok(result);
        }
    }
}