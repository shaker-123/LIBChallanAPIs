using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LIBChallanAPIs.Controllers
{
    [ApiController]
    [Route("api/state-masters")]
    [Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE")]
    public class StateMastersController : ControllerBase
    {
        private readonly IStateMasterRepository _repository;

        public StateMastersController(IStateMasterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("active/{countryId}")]
        public async Task<IActionResult> GetActive(string countryId)
        {
            var result = await _repository.GetAllActiveAsync(countryId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StateMasterCreateDto dto)
        {
            var result = await _repository.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StateMasterUpdateDto dto)
        {
            var result = await _repository.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok("Deleted Successfully");
        }

        [HttpPatch("toggle")]
        public async Task<IActionResult> Toggle(StateMasterToggleActiveDto dto)
        {
            var result = await _repository.ToggleActiveAsync(dto);
            if (!result) return NotFound();
            return Ok("Status Updated");
        }
    }
}
