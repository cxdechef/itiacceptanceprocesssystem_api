using ITIAcceptanceProcessSystem.DTOs;
using ITIAcceptanceProcessSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITIAcceptanceProcessSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CandidateDto candidateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.RegisterCandidateAsync(candidateDto);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var token = await _authService.LoginAsync(loginDto);
            if (token == null) return Unauthorized();

            return Ok(new { token });
        }
    }
}
