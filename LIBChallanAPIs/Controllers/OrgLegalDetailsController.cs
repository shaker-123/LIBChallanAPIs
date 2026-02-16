using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LIBChallanAPIs.Controllers
{
    [ApiController]
    [Route("api/org-legal-details")]
    [Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE")]
    public class OrgLegalDetailsController : ControllerBase
    {
        private readonly IOrgLegalDetailsRepository _repository;

        public OrgLegalDetailsController(IOrgLegalDetailsRepository repository)
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
            => Ok(await _repository.GetAllPagedAsync(request));

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
            => Ok(await _repository.GetAllActiveAsync());

        [HttpPost]
        public async Task<IActionResult> Create(OrgLegalDetailsCreateDto dto)
            => Ok(await _repository.CreateAsync(dto));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, OrgLegalDetailsUpdateDto dto)
        {
            var result = await _repository.UpdateAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => await _repository.DeleteAsync(id) ? NoContent() : NotFound();

        [HttpPatch("toggle-active")]
        public async Task<IActionResult> ToggleActive(ToggleActiveDto dto)
            => await _repository.ToggleActiveAsync(dto)
                ? Ok(new { message = "Status updated successfully" })
                : NotFound();
    }
}
