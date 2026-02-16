using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }

}
