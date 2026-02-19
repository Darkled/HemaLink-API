using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("register-moderator")]
        public async Task<ActionResult<ResponseDto<StaffResponseDto>>> Register(ModeratorRegistrationRequestDto request)
        {
            var result = await _adminService.RegisterModeratorAsync(request);

            if (!result.Success)
                return BadRequest(new ResponseDto<string>
                {
                    Status = 400,
                    Message = result.Error,
                    Data = null
                });

            return Ok(new ResponseDto<StaffResponseDto>
            {
                Status = 200,
                Message = "Moderator registered successfully",
                Data = result.Data
            });
        }

        [HttpPut("promote-moderator")]
        public async Task<ActionResult<ResponseDto<StaffResponseDto>>> Promote(ModeratorPromotionDto request)
        {
            var result = await _adminService.PromoteModeratorAsync(request);

            if (!result.Success)
                return BadRequest(new ResponseDto<string>
                {
                    Status = 400,
                    Message = result.Error,
                    Data = null
                });

            return Ok(new ResponseDto<StaffResponseDto>
            {
                Status = 200,
                Message = "Moderator promoted successfully",
                Data = result.Data
            });
        }

        [HttpGet("users")]
        public async Task<ActionResult<ResponseDto<List<AccountResponseDto>>>> GetUsers([FromQuery] Role? role)
        {
            var result = await _adminService.GetAllUsersAsync(role);

            return Ok(new ResponseDto<List<AccountResponseDto>>
            {
                Status = 200,
                Message = "Success",
                Data = result.Data
            });
        }

        [HttpGet("user-by-email")]
        public async Task<ActionResult<ResponseDto<AccountResponseDto>>> GetByEmail([FromQuery] string email)
        {
            var result = await _adminService.GetUserAsync(email);

            if (!result.Success)
                return NotFound(new ResponseDto<string>
                {
                    Status = 404,
                    Message = result.Error,
                    Data = null
                });

            return Ok(new ResponseDto<AccountResponseDto>
            {
                Status = 200,
                Message = "Success",
                Data = result.Data
            });
        }

        [HttpDelete("delete-account")]
        public async Task<ActionResult<ResponseDto<string>>> Delete([FromQuery] string email)
        {
            var result = await _adminService.DeleteAccountAsync(email);

            if (!result.Success)
                return NotFound(new ResponseDto<string>
                {
                    Status = 404,
                    Message = result.Error,
                    Data = null
                });

            return Ok(new ResponseDto<string>
            {
                Status = 200,
                Message = "Account deleted successfully",
                Data = null
            });
        }
    }
}
