using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIBChallanAPIs.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE")]
    public class RolesController : ControllerBase
    {
        private readonly RoleRepository _repo;
        public RolesController(RoleRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repo.GetAllAsync());
    }

}
