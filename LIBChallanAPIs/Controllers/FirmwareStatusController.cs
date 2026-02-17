using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LIBChallanAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE")]
    public class FirmwareStatusController : ControllerBase
    {
        private readonly IFirmwareStatusRepository _repository;

        public FirmwareStatusController(IFirmwareStatusRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FirmwareStatus model)
        {
            var created = await _repository.CreateAsync(model);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FirmwareStatus model)
        {
            var updated = await _repository.UpdateAsync(id, model);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return Ok("Deleted Successfully");
        }
    }
}
