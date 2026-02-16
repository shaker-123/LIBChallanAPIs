using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

public class UserTypeRepository : IUserTypeRepository
{
    private readonly AppDbContext _context;

    public UserTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserTypeDto>> GetAllAsync()
    {
        return await _context.UserTypes
            .OrderBy(x => x.TypeName)
            .Select(x => new UserTypeDto
            {
                Id = x.Id,
                TypeId = x.TypeId,
                TypeName = x.TypeName,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }

    public async Task<UserTypeDto?> GetByIdAsync(int id)
    {
        return await _context.UserTypes
            .Where(x => x.Id == id)
            .Select(x => new UserTypeDto
            {
                Id = x.Id,
                TypeId = x.TypeId,
                TypeName = x.TypeName,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync();
    }

    public async Task<UserTypeDto> CreateAsync(UserTypeCreateDto dto, int userId)
    {
        var lastId = await _context.UserTypes
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        var nextId = lastId + 1;

        var entity = new UserType
        {
            TypeId = $"UTM{nextId:D3}", 
            TypeName = dto.TypeName,
            Description = dto.Description,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        _context.UserTypes.Add(entity);
        await _context.SaveChangesAsync();

        return new UserTypeDto
        {
            Id = entity.Id,
            TypeId = entity.TypeId,
            TypeName = entity.TypeName,
            Description = entity.Description,
            IsActive = entity.IsActive
        };
    }

    public async Task<UserTypeDto?> UpdateAsync(int id, UserTypeUpdateDto dto, int userId)
    {
        var entity = await _context.UserTypes.FindAsync(id);
        if (entity == null) return null;

        entity.TypeName = dto.TypeName ?? entity.TypeName;
        entity.Description = dto.Description ?? entity.Description;
        entity.IsActive = dto.IsActive ?? entity.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.UserTypes.FindAsync(id);
        if (entity == null) return false;

        _context.UserTypes.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleActiveAsync(int id, bool isActive, int userId)
    {
        var entity = await _context.UserTypes.FindAsync(id);
        if (entity == null) return false;

        entity.IsActive = isActive;

        await _context.SaveChangesAsync();
        return true;
    }
}
