using Microsoft.AspNetCore.Mvc;
using wms_android.api.Interfaces;
using wms_android.shared.Interfaces;
using wms_android.shared.Models;
using wms_android.shared.Services;
using System;
using System.Threading.Tasks;

namespace wms_android.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService; // API's DB-backed user service
        private readonly ITokenService _tokenService; // Inject API's ITokenService

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            if (credentials == null || string.IsNullOrWhiteSpace(credentials.Username) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest("Username and password are required.");
            }

            // Use the API's UserService to check against DB
            var user = await _userService.AuthenticateAsync(credentials.Username, credentials.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            try
            {
                // Use the injected ITokenService to generate the token
                var token = await _tokenService.GenerateJwtTokenAsync(credentials.Username, credentials.Password);

                // Return token and basic user info
                return Ok(new
                {
                    Token = token,
                    Username = user.Username,
                    Role = user.Role?.Name ?? "User",
                    UserId = user.Id 
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred during token generation.");
            }
        }
    }
}
