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

        [HttpGet("users")]
        public async Task<ActionResult<ResponseDto<List<AccountResponseDto>>>> GetUsers([FromQuery] Role? role)
        {
            var result = await _adminService.GetAllUsersAsync(role);

            return Ok(new ResponseDto<List<AccountResponseDto>>
            {
                Status = 200,
                Message = "Success",
                Data = result
            });
        }
        
        [HttpGet("user-by-email")]
        public async Task<ActionResult<ResponseDto<AccountResponseDto>>> GetByEmail([FromQuery] string email)
        {
            var user = await _adminService.GetUserAsync(email);

            if (user == null)
            {
                return NotFound(new ResponseDto<string>
                {
                    Status = 404,
                    Message = "User not found",
                    Data = null
                });
            }

            return Ok(new ResponseDto<AccountResponseDto>
            {
                Status = 200,
                Message = "Success",
                Data = user
            });
        }
        
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
