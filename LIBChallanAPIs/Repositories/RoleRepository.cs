using LIBChallanAPIs.DTOs;
using Microsoft.EntityFrameworkCore;

public class RoleRepository
{
    private readonly AppDbContext _context;
    public RoleRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<RoleDto>> GetAllAsync()
        => await _context.MasterRoles
            .Select(x => new RoleDto
            {
                RoleId = x.RoleId,
                RoleName = x.RoleName,
                IsActive = x.IsActive
            }).ToListAsync();
}
