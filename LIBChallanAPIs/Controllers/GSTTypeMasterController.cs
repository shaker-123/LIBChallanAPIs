using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LIBChallanAPIs.Controllers
{
    [ApiController]
    [Route("api/gst-types")]
    [Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE")]
    public class GSTTypeMasterController : ControllerBase
    {
        private readonly IGSTTypeMasterRepository _repository;

        public GSTTypeMasterController(IGSTTypeMasterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var result = await _repository.GetAllActiveAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GSTTypeCreateDto dto)
        {
            var result = await _repository.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] GSTTypeUpdateDto dto)
        {
            var result = await _repository.UpdateAsync(id, dto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("toggle-active")]
        public async Task<IActionResult> ToggleActive([FromBody] ToggleActiveDto dto)
        {
            var success = await _repository.ToggleActiveAsync(dto);
            if (!success)
                return NotFound();

            return Ok(new { message = "GST Type status updated successfully" });
        }
    }
}
