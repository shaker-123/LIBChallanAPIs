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

        var user = await _context.MasterUsers
            .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.IsActive);

        if (user == null)
            return NotFound("User not found");

        var role = await _context.MasterRoles
            .FirstOrDefaultAsync(r => r.RoleId == dto.RoleId && r.IsActive);

        if (role == null)
            return NotFound("Role does not exist");

        var alreadyAssigned = await _context.MasterUserRoles
            .AnyAsync(x => x.UserRefId == user.Id && x.RoleId == role.RoleId);

        if (alreadyAssigned)
            return BadRequest("Role already assigned");

        _context.MasterUserRoles.Add(new UserRole
        {
            UserRefId = user.Id,     
            RoleId = role.RoleId,     
            CreatedAt = DateTime.UtcNow,
            CreatedBy = null 
        });

        await _context.SaveChangesAsync();

        return Ok("Role assigned successfully");
    }

    [HttpGet("users-by-role/{roleName}")]
    public async Task<IActionResult> GetUsersByRole(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return BadRequest("Role name is required");

        var users = await _context.MasterUserRoles
            .Where(ur => ur.Role.RoleName == roleName && ur.Role.IsActive)
            .Select(ur => new
            {
                ur.User.UserId,
                ur.User.UserName,
                ur.User.FullName,
                ur.User.Email,
                ur.User.Phone
            })
            .Distinct()
            .ToListAsync();

        if (!users.Any())
            return NotFound("No users found for this role");

        return Ok(users);
    }

    [HttpGet("fse-users")]
    public async Task<IActionResult> GetFSEUsers()
    {
        var users = await _context.MasterUserRoles
            .Where(ur => ur.Role.RoleName == "FSE_ROLE"
                         && ur.Role.IsActive
                         && ur.User.IsActive)
            .Select(ur => new
            {
                ur.User.UserId,
                ur.User.UserName,
                ur.User.FullName,
                ur.User.Email,
                ur.User.Phone
            })
            .Distinct()
            .ToListAsync();

        if (!users.Any())
            return NotFound("No FSE users found");

        return Ok(users);
    }



    [HttpGet("GetAllUserList")]
    public async Task<List<AppUser>> GetAllUserList()
    {
        return await _context.MasterUsers
            .Where(u => u.IsActive)
            .ToListAsync();
    }
}
