using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/user-roles")]
[Authorize(Roles = "SUPER_USER,ADMIN")]
public class UserRoleController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserRoleController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole(AssignRoleDto dto)
    {
        var user = await _context.MasterUsers
            .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.IsActive);

        if (user == null)
            return NotFound("User not found");

        var role = await _context.MasterRoles
            .FirstOrDefaultAsync(r => r.RoleId == dto.RoleId);

        if (role == null)
            return NotFound("Role does not exist");

        var alreadyAssigned = await _context.MasterUserRoles
            .AnyAsync(x => x.UserId == dto.UserId && x.RoleId == role.RoleId);

        if (alreadyAssigned)
            return BadRequest("Role already assigned");

        _context.MasterUserRoles.Add(new UserRole
        {
            UserId = dto.UserId,
            RoleId = role.RoleId
        });

        await _context.SaveChangesAsync();

        return Ok("Role assigned successfully");
    }

    [HttpGet("GetAllUserList")]
    public async Task<List<AppUser>> GetAllUserList()
    {
        List<AppUser> dummyList = new List<AppUser>();
        dummyList = await _context.MasterUsers.ToListAsync();
        return dummyList;
    }
}
