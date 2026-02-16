using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIBChallanAPIs.Controllers
{
    [ApiController]
    [Route("api/user-types")]
    [Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE")]
    public class UserTypesController : ControllerBase
    {
        private readonly IUserTypeRepository _repo;

        public UserTypesController(IUserTypeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create(UserTypeCreateDto dto)
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            return Ok(await _repo.CreateAsync(dto, userId));
        }
    }

}
