using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public DonorController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("blood-requests")]
        public async Task<IActionResult> GetOpenRequests([FromQuery] List<BloodType> bloodTypes)
        {
            Result<List<BloodRequestResponseDto>> result = await _requestService.GetOpenRequestsAsync(bloodTypes);

            if (!result.Success)
                return NotFound(ResponseDto<AccountResponseDto>.Fail(result.Error));

            return Ok(ResponseDto<List<BloodRequestResponseDto>>.Ok(result.Data, "Blood requests retrieved succesfully"));
        }

        [HttpPost("blood-requests")]
        public async Task<IActionResult> AppointBloodRequest([FromBody] AppointBloodRequestRequestDto appointBloodRequestDto)
        {
            Result<AppointBloodRequestResponseDto> result = await _requestService.AppointBloodRequestAsync(appointBloodRequestDto);

            if (!result.Success)
                return BadRequest(ResponseDto<AccountResponseDto>.Fail(result.Error));

            return Ok(ResponseDto<AppointBloodRequestResponseDto>.Ok(result.Data, "Appointment created succesfully"));
        }

        [HttpPut("appointment/cancel/{requestId}")]
        public async Task<IActionResult> CancelBloodRequest(int requestId, [FromQuery] string cancellationToken)
        {
            Result<bool> result = await _requestService.CancelAppointmentAsync(requestId, cancellationToken);
            if (!result.Success)
                return BadRequest(ResponseDto<bool>.Fail(result.Error));
            return Ok(ResponseDto<bool>.Ok(result.Data, "Blood request cancelled succesfully"));
        }
    }
}

