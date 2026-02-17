using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;

namespace LIBChallanAPIs.Controllers
{
    [ApiController]
    [Route("api/service-activities")]
    [Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE,FSE_ROLE")]
    public class ServiceActivityController : ControllerBase
    {
        private readonly IServiceActivityRepository _repository;

        public ServiceActivityController(IServiceActivityRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceActivityCreateDto dto)
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var activityId = await _repository.CreateAsync(dto, userId);

            return Ok(new
            {
                Message = "Service Activity Created Successfully",
                ActivityId = activityId
            });
        }

        [HttpPatch("{activityId}/batteries/{batterySerial}")]
        public async Task<IActionResult> UpdateSingleBattery([FromQuery] string activityId, string batteryId, [FromBody] BatteryTranUpdateDto batteryDto)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                await _repository.UpdateSingleBatteryAsync(batteryId, batteryDto);
                return Ok(new { Message = "Battery updated successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("{activityId}")]
        public async Task<IActionResult> GetActivityById(string activityId)
        {
            var activity = await _repository.GetActivityByIdAsync(activityId);
            if (activity == null)
                return NotFound(new { Message = $"Activity '{activityId}' not found." });

            return Ok(activity);
        }

        [HttpGet("my-activities")]
        public async Task<IActionResult> GetMyActivities()
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var activities = await _repository.GetActivitiesByUserIdAsync(userId);
            return Ok(activities);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagedRequest request)
        {
            var pagedResult = await _repository.GetPagedActivitiesAsync(request);

            return Ok(pagedResult);
        }


        [HttpGet("GetToeken")]
        public async Task<IActionResult> GetAllToeken()
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "http://lib-trace.i-diligence.com/api/account/login");


            var json = "{\"userName\": \"LIB-VIEW-DETAILS\", \"password\": \"Lib@1234\"}";
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return Ok(responseContent);
        }

    }

}
