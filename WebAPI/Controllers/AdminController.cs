using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("moderator")]
        public async Task<ActionResult<ResponseDto<AccountResponseDto>>> RegisterModerator([FromBody] ModeratorRegistrationRequestDto request)
        {
            Result<AccountResponseDto> result = await _adminService.RegisterModeratorAsync(request);

            if (!result.Success)
                return BadRequest(ResponseDto<AccountResponseDto>.Fail(result.Error));

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result.Data!.Id },
                ResponseDto<AccountResponseDto>.Ok(result.Data, "Moderator registered successfully")
            );
        }

        [HttpPut("moderator/promote")]
        public async Task<ActionResult<ResponseDto<AccountResponseDto>>> PromoteModerator([FromBody] ModeratorPromotionRequestDto request)
        {
            Result<AccountResponseDto> result = await _adminService.PromoteModeratorAsync(request);

            if (!result.Success)
                return BadRequest(ResponseDto<AccountResponseDto>.Fail(result.Error));

            return Ok(ResponseDto<AccountResponseDto>.Ok(result.Data!, "Moderator promoted successfully"));
        }

        [HttpGet("users")]
        public async Task<ActionResult<ResponseDto<List<AccountResponseDto>>>> GetUsers([FromQuery] Role? role)
        {
            Result<List<AccountResponseDto>> result = await _adminService.GetAllUsersAsync(role);

            return Ok(ResponseDto<List<AccountResponseDto>>.Ok(result.Data!, "Users retrieved successfully"));
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResponseDto<AccountResponseDto>>> GetUserById([FromRoute] int id)
        {
            Result<AccountResponseDto> result = await _adminService.GetUserAsync(id);

            if (!result.Success)
                return NotFound(ResponseDto<AccountResponseDto>.Fail(result.Error));

            return Ok(ResponseDto<AccountResponseDto>.Ok(result.Data!, "User retrieved successfully"));
        }

        [HttpGet("user/email")]
        public async Task<ActionResult<ResponseDto<AccountResponseDto>>> GetUserByEmail([FromQuery] string email)
        {
            Result<AccountResponseDto> result = await _adminService.GetUserAsync(email);

            if (!result.Success)
                return NotFound(ResponseDto<AccountResponseDto>.Fail(result.Error));

            return Ok(ResponseDto<AccountResponseDto>.Ok(result.Data!, "User retrieved successfully"));
        }

        [HttpDelete("users/{id}")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteUser(int id)
        {
            int currentUserId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            Result<bool> result = await _adminService.DeleteAccountAsync(id, currentUserId);

            if (!result.Success)
                return BadRequest(ResponseDto<string>.Fail(result.Error));

            return Ok(ResponseDto<string>.Ok(null, "Account deleted successfully"));
        }
    }
}
