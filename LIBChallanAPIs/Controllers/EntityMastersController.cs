using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LIBChallanAPIs.Controllers
{
    [ApiController]
    [Route("api/customer-masters")]
    [Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE")]
    public class EntityMastersController : ControllerBase
    {
        private readonly IEntityMasterRepository _repository;

        public EntityMastersController(IEntityMasterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetAllPaged([FromQuery] PagedRequest request)
        {
            return Ok(await _repository.GetAllPagedAsync(request));
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            return Ok(await _repository.GetAllActiveAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EntityMasterCreateDto dto)
        {
            var result = await _repository.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, EntityMasterUpdateDto dto)
        {
            var result = await _repository.UpdateAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _repository.DeleteAsync(id) ? NoContent() : NotFound();
        }

        [HttpPatch("toggle-active")]
        public async Task<IActionResult> ToggleActive(ToggleActiveDto dto)
        {
            return await _repository.ToggleActiveAsync(dto)
                ? Ok(new { message = "Status updated" })
                : NotFound();
        }
    }
}
