using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class StateMasterRepository : IStateMasterRepository
    {
        private readonly AppDbContext _context;

        public StateMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StateMasterDto?> GetByIdAsync(int id)
        {
            return await _context.StateMasters
                .Where(x => x.Id == id)
                .Select(x => new StateMasterDto
                {
                    Id = x.Id,
                    StateId = x.StateId,
                    StateName = x.StateName,
                    CountryId = x.CountryId,
                    Region = x.Region,
                    GstCode = x.GstCode,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StateMasterDto>> GetAllActiveAsync(string countryId)
        {
            return await _context.StateMasters
                .Where(x => x.IsActive && x.CountryId == countryId)
                .OrderBy(x => x.StateName)
                .Select(x => new StateMasterDto
                {
                    Id = x.Id,
                    StateId = x.StateId,
                    StateName = x.StateName,
                    CountryId = x.CountryId,
                    Region = x.Region,
                    GstCode = x.GstCode,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<StateMasterDto> CreateAsync(StateMasterCreateDto dto)
        {
            var lastId = await _context.StateMasters
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var nextId = lastId + 1;

            var entity = new StateMaster
            {
                StateId = $"STM{nextId:D3}",
                StateName = dto.StateName,
                CountryId = dto.CountryId,
                Region = dto.Region,
                GstCode = dto.GstCode,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.StateMasters.Add(entity);
            await _context.SaveChangesAsync();

            return new StateMasterDto
            {
                Id = entity.Id,
                StateId = entity.StateId,
                StateName = entity.StateName,
                CountryId = entity.CountryId,
                Region = entity.Region,
                GstCode = entity.GstCode,
                IsActive = entity.IsActive
            };
        }

        public async Task<StateMasterDto?> UpdateAsync(int id, StateMasterUpdateDto dto)
        {
            var entity = await _context.StateMasters.FindAsync(id);
            if (entity == null) return null;

            entity.StateName = dto.StateName ?? entity.StateName;
            entity.Region = dto.Region ?? entity.Region;
            entity.GstCode = dto.GstCode ?? entity.GstCode;
            entity.IsActive = dto.IsActive ?? entity.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StateMasters.FindAsync(id);
            if (entity == null) return false;

            _context.StateMasters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(StateMasterToggleActiveDto dto)
        {
            var entity = await _context.StateMasters
                .FirstOrDefaultAsync(x => x.StateId == dto.StateId);

            if (entity == null) return false;

            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
