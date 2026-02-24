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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Requester")]
    public class RequesterController : ControllerBase
    {
        private readonly IRequesterService _requesterService;

        public RequesterController(IRequesterService requesterService)
        {
            _requesterService = requesterService;
        }

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

        [HttpPut("blood-requests/{requestId}")]
        public async Task<ActionResult<ResponseDto<BloodRequestResponseDto>>> UpdateBloodRequest(int requestId, [FromBody] BloodRequestRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int requesterId = int.Parse(userIdClaim.Value);

            Result<BloodRequestResponseDto> result = await _requesterService.UpdateBloodRequestAsync(requestId, requesterId, request);
            if (!result.Success)
                return BadRequest(ResponseDto<BloodRequestResponseDto>.Fail(result.Error));
            return Ok(ResponseDto<BloodRequestResponseDto>.Ok(result.Data, "Blood request updated succesfully"));
        }

        [HttpGet("blood-requests/own")]
        public async Task<ActionResult<ResponseDto<List<BloodRequestResponseDto>>>> GetByRequester([FromQuery] List<RequestStatus> statuses)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            Result<List<BloodRequestResponseDto>> result = await _requesterService.GetBloodRequestsByRequesterIdAsync(userId, statuses);

            if (!result.Success)
                return BadRequest(ResponseDto<List<BloodRequestResponseDto>>.Fail(result.Error));

            return Ok(ResponseDto<List<BloodRequestResponseDto>>.Ok(result.Data, "Your blood requests retrieved successfully"));
        }

        [HttpPut("blood-requests/cancel/{requestId}")]
        public async Task<ActionResult<ResponseDto<BloodRequestResponseDto>>> CancelBloodRequest(int requestId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int requesterId = int.Parse(userIdClaim.Value);
            Result<BloodRequestResponseDto> result = await _requesterService.CancelBloodRequestAsync(requestId, requesterId);
            if (!result.Success)
                return BadRequest(ResponseDto<bool>.Fail(result.Error));
            return Ok(ResponseDto<BloodRequestResponseDto>.Ok(result.Data, "Blood request deleted successfully"));
        }

        [HttpGet("donors")]
        public async Task<ActionResult<ResponseDto<List<DonorResponseDto>>>> GetDonors([FromQuery] int bloodRequestId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            Result<List<DonorResponseDto>> result = await _requesterService.GetDonorsFromBloodRequestAsync(bloodRequestId, userId);

            if (!result.Success)
                return BadRequest(ResponseDto<List<DonorResponseDto>>.Fail(result.Error));

            return Ok(ResponseDto<List<DonorResponseDto>>.Ok(result.Data, "Donors retrieved succesfully"));
        }

        [HttpGet("all-donors")]
        public async Task<ActionResult<ResponseDto<List<DonorResponseDto>>>> GetAllDonors([FromQuery] int bloodRequestId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            Result<List<DonorResponseDto>> result = await _requesterService.GetDonorsFromRequesterAsync(userId);

            if (!result.Success)
                return BadRequest(ResponseDto<List<DonorResponseDto>>.Fail(result.Error));

            return Ok(ResponseDto<List<DonorResponseDto>>.Ok(result.Data, "Donors retrieved succesfully"));
        }
    }
}
