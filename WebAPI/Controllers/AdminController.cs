using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register-moderator")]
        public async Task<ActionResult<ResponseDto<StaffResponseDto>>> Register(ModeratorRegistrationRequestDto request)
        {
            try
            {
                StaffResponseDto response = await _adminService.RegisterModeratorAsync(request);
                return Ok(new ResponseDto<StaffResponseDto>
                {
                    Status = 200,
                    Message = "Success",
                    Data = response
                });
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(new ResponseDto<string>
                {
                    Status = 400,
                    Message = exception.Message,
                    Data = null
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("promote-moderator")]
        public async Task<ActionResult<ResponseDto<StaffResponseDto>>> Promote(ModeratorPromotionDto request)
        {
            try
            {
                StaffResponseDto response = await _adminService.PromoteModeratorAsync(request);
                return Ok(new ResponseDto<StaffResponseDto>
                {
                    Status = 200,
                    Message = "Success",
                    Data = response
                });
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(new ResponseDto<string>
                {
                    Status = 400,
                    Message = exception.Message,
                    Data = null
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users-list")]
        public async Task<ActionResult<ResponseDto<List<AccountListDto>>>> GetUsers([FromQuery] string? role)
        {
            var result = await _adminService.GetUsersByRoleAsync(role ?? "");

            return Ok(new ResponseDto<List<AccountListDto>>
            {
                Status = 200,
                Message = "Success",
                Data = result
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("user-by-email")]
        public async Task<ActionResult<ResponseDto<AccountListDto>>> GetByEmail([FromQuery] string email)
        {
            var user = await _adminService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound(new ResponseDto<string>
                {
                    Status = 404,
                    Message = "User not found",
                    Data = null
                });
            }

            return Ok(new ResponseDto<AccountListDto>
            {
                Status = 200,
                Message = "Success",
                Data = user
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-account")]
        public async Task<ActionResult<ResponseDto<string>>> Delete([FromQuery] string email)
        {
            try
            {
                await _adminService.DeleteAccountAsync(email);

                return Ok(new ResponseDto<string>
                {
                    Status = 200,
                    Message = "Account deleted successfully",
                    Data = null
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ResponseDto<string>
                {
                    Status = 400,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }
}
