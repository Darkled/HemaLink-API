using Application;
using Application.Interfaces;
using Application.Models;
using Application.Models.Responses;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonatorController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public DonatorController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("blood-requests")]
        public async Task<IActionResult> GetOpenRequests([FromQuery] List<BloodType>? bloodTypes)
        {
            Result<List<BloodRequestResponseDto>> result = await _requestService.GetOpenRequestsAsync(bloodTypes);

            if (!result.Success)
                return NotFound(ResponseDto<AccountResponseDto>.Fail(result.Error));

            return Ok(ResponseDto<List<BloodRequestResponseDto>>.Ok(result.Data, "Blood requests retrieved succesfully"));
        }

    }
}

