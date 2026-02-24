using Application;
using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ModeratorPolicy")]
    public class ModeratorController : ControllerBase
    {
        private readonly IModeratorService _moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }

        [HttpPut("accept")]
        public async Task<ActionResult> AcceptRequester([FromQuery] int id)
        {
            var result = await _moderatorService.ValidateRequesterAsync(id, true);
            if (!result.Success)
                return BadRequest(result.Error);
            return Ok("Requester accepted successfully");
        }

        [HttpPut("reject")]
        public async Task<ActionResult> RejectRequester([FromQuery] int id)
        {
            var result = await _moderatorService.ValidateRequesterAsync(id, false);
            if (!result.Success)
                return BadRequest(result.Error);
            return Ok("Requester rejected successfully");
        }

        [HttpGet("pending-requesters")]
        public async Task<ActionResult> GetPendingRequesters()
        {
            var result = await _moderatorService.GetPendingRequestersAsync();
            if (!result.Success)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("requesters")]
        public async Task<ActionResult> GetAllRequesters()
        {
            var result = await _moderatorService.GetAllRequestersAsync();
            if (!result.Success)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpPost("blood-requests/")]
        public async Task<ActionResult<ResponseDto<BloodRequestResponseDto>>> AddBloodRequest([FromQuery] int requesterId, [FromBody] BloodRequestRequestDto request)
        {
            Result<BloodRequestResponseDto> result = await _moderatorService.AddBloodRequestAsync(request, requesterId);

            if (!result.Success)
                return BadRequest(ResponseDto<BloodRequestRequestDto>.Fail(result.Error));

            return Ok(ResponseDto<BloodRequestResponseDto>.Ok(result.Data, "Blood request added succesfully"));
        }

        [HttpPut("blood-requests/{requestId}")]
        public async Task<ActionResult<ResponseDto<BloodRequestResponseDto>>> UpdateBloodRequest(int requestId, [FromBody] BloodRequestRequestDto request)
        {
            Result<BloodRequestResponseDto> result = await _moderatorService.UpdateBloodRequestAsync(requestId, request);
            if (!result.Success)
                return BadRequest(ResponseDto<BloodRequestResponseDto>.Fail(result.Error));
            return Ok(ResponseDto<BloodRequestResponseDto>.Ok(result.Data, "Blood request updated succesfully"));
        }

        [HttpDelete("blood-requests/{requestId}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteBloodRequest(int requestId)
        {
            Result<bool> result = await _moderatorService.DeleteBloodRequestAsync(requestId);
            if (!result.Success)
                return BadRequest(ResponseDto<bool>.Fail(result.Error));
            return Ok(ResponseDto<bool>.Ok(result.Data, "Blood request deleted successfully"));
        }

        [HttpGet("donors")]
        public async Task<ActionResult<ResponseDto<List<DonorResponseDto>>>> GetDonors([FromQuery] int bloodRequestId)
        {
            Result<List<DonorResponseDto>> result = await _moderatorService.GetDonorsFromBloodRequestAsync(bloodRequestId);

            if (!result.Success)
                return BadRequest(ResponseDto<List<DonorResponseDto>>.Fail(result.Error));

            return Ok(ResponseDto<List<DonorResponseDto>>.Ok(result.Data, "Your blood requests retrieved successfully"));
        }
    }
}