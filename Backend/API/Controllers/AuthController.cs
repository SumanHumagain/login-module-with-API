using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Constructor for AuthController, injecting an IAuthService instance
        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("Validate")]
        public IActionResult Validate([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                // Return a 400 Bad Request with a meaningful message
                return BadRequest(new { Message = "Invalid request" });
            }

            bool isValid = _authService.ValidateCredentials(request.Username, request.Password);

            // If credentials are valid, return a 200 OK with a success message
            if (isValid)
            {
                return Ok(new { Message = "User validated successfully" });
            }

            // If credentials are invalid, return a 401 Unauthorized with an error message
            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }

    // Model class representing the login request body
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
