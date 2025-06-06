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

            if (result is string error)
            {
                return error switch
                {
                    "user" => BadRequest("Login model is missing."),
                    "role" => Unauthorized("Invalid User Role."),
                    "username" => Unauthorized("Username not found."),
                    "password" => Unauthorized("Incorrect password."),
                    _ => Unauthorized("Invalid credentials.")
                };
            }

            var loginResult = result as LoginInResponce;
            return Ok(new { Token = loginResult!.Token, User = loginResult.User, Message = "Login successful." });
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