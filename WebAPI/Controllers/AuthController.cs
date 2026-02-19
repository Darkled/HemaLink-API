using Application.Models.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Models;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto<string>>> Login([FromBody] LoginRequestDto request)
        {
            Result<string> result = await _authService.LoginAsync(request);

            if (!result.Success)
                return Unauthorized(ResponseDto<string>.Fail(result.Error));

            return Ok(ResponseDto<string>.Ok(result.Data!, "Login successful"));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto<string>>> Register([FromBody] RequesterRegistrationRequestDto request)
        {
            Result<string> result = await _authService.RegisterRequesterAsync(request);

            if (!result.Success)
                return BadRequest(ResponseDto<string>.Fail(result.Error));

            return Ok(ResponseDto<string>.Ok(result.Data!, "Registration successful"));
        }
    }
}
