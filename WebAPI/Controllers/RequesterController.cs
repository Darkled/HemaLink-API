using Application;
using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequesterPolicy")]
    public class RequesterController : ControllerBase
    {
        private readonly IRequesterService _requesterService;

        public RequesterController(IRequesterService requesterService)
        {
            _requesterService = requesterService;
        }

        [Authorize(Roles = "Requester")]
        [HttpPost("blood-requests")]
        public async Task<ActionResult<ResponseDto<BloodRequestResponseDto>>> AddBloodRequest([FromBody] BloodRequestRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int requesterId = int.Parse(userIdClaim.Value);

            Result<BloodRequestResponseDto> result = await _requesterService.AddBloodRequestAsync(request, requesterId);

            if (!result.Success)
                return BadRequest(ResponseDto<BloodRequestRequestDto>.Fail(result.Error));

            return Ok(ResponseDto<BloodRequestResponseDto>.Ok(result.Data, "Blood request added succesfully"));
        }

        [Authorize(Roles = "Requester")]
        [HttpGet("blood-requests/own")]
        public async Task<ActionResult<ResponseDto<List<BloodRequestResponseDto>>>> GetByRequester()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            Result<List<BloodRequestResponseDto>> result = await _requesterService.GetBloodRequestsByRequesterIdAsync(userId);

            if (!result.Success)
                return BadRequest(ResponseDto<List<BloodRequestResponseDto>>.Fail(result.Error));

            return Ok(ResponseDto<List<BloodRequestResponseDto>>.Ok(result.Data, "Your blood requests retrieved successfully"));
        }
    }
}
