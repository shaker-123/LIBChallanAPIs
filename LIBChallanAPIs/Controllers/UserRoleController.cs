using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/user-roles")]
[Authorize(Roles = "SUPER_ROLE,ADMIN_ROLE")]
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
        if (string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.RoleId))
            return BadRequest("UserId and RoleId are required");

        // Get user by business UserId
        var user = await _context.MasterUsers
            .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.IsActive);

        if (user == null)
            return NotFound("User not found");

        // Get role by business RoleId
        var role = await _context.MasterRoles
            .FirstOrDefaultAsync(r => r.RoleId == dto.RoleId && r.IsActive);

        if (role == null)
            return NotFound("Role does not exist");

        // Check if role already assigned
        var alreadyAssigned = await _context.MasterUserRoles
            .AnyAsync(x => x.UserRefId == user.Id && x.RoleId == role.RoleId);

        if (alreadyAssigned)
            return BadRequest("Role already assigned");

        // Assign role
        _context.MasterUserRoles.Add(new UserRole
        {
            UserRefId = user.Id,      // FK to AppUser.Id
            RoleId = role.RoleId,      // string FK to Role.RoleId
            CreatedAt = DateTime.UtcNow,
            CreatedBy = null // TODO: set current user Id if available
        });

        await _context.SaveChangesAsync();

        return Ok("Role assigned successfully");
    }

    [HttpGet("GetAllUserList")]
    public async Task<List<AppUser>> GetAllUserList()
    {
        return await _context.MasterUsers
            .Where(u => u.IsActive)
            .ToListAsync();
    }
}
