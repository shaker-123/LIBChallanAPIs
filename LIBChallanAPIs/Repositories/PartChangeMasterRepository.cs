using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class PartChangeMasterRepository : IPartChangeMasterRepository
    {
        private readonly AppDbContext _context;

        public PartChangeMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PartChangeMasterDto?> GetByIdAsync(int id)
        {
            return await _context.PartChangeMasters
                .Where(x => x.Id == id)
                .Select(x => new PartChangeMasterDto
                {
                    Id = x.Id,
                    PartId = x.PartId,
                    PartName = x.PartName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PartChangeMasterDto?> GetByPartNameAsync(string PartName)
        {
            return await _context.PartChangeMasters
                .Where(x => x.PartName == PartName)
                .Select(x => new PartChangeMasterDto
                {
                    Id = x.Id,
                    PartId = x.PartId,
                    PartName = x.PartName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<PartChangeMasterDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<PartChangeMaster> query = _context.PartChangeMasters;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x =>
                    x.PartName.Contains(request.SearchValue));
            }

            query = query.OrderBy(x => x.PartName);

            switch (request.Status)
            {
                case RecordStatus.Active:
                    query = query.Where(x => x.IsActive == true);
                    break;

                case RecordStatus.Inactive:
                    query = query.Where(x => x.IsActive == false);
                    break;

                case RecordStatus.All:
                default:
                    break;
            }

            var totalRecords = await query.CountAsync();

            var data = await query
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => new PartChangeMasterDto
                {
                    Id = x.Id,
                    PartId = x.PartId,
                    PartName = x.PartName,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<PartChangeMasterDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<IEnumerable<PartChangeMasterDto>> GetAllActiveAsync()
        {
            return await _context.PartChangeMasters
                .Where(x => x.IsActive)
                .OrderBy(x => x.PartName)
                .Select(x => new PartChangeMasterDto
                {
                    Id = x.Id,
                    PartId = x.PartId,
                    PartName = x.PartName,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<PartChangeMasterDto> CreateAsync(PartChangeMasterCreateDto dto)
        {
            if (await _context.PartChangeMasters
                .AnyAsync(x => x.PartName == dto.PartName))
                throw new ArgumentException("Part name already exists.");

            var lastId = await _context.PartChangeMasters
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var nextId = lastId + 1;

            var entity = new PartChangeMaster
            {
                PartId = $"PCM{nextId:D3}",
                PartName = dto.PartName,
                IsActive = dto.IsActive,
            };

            _context.PartChangeMasters.Add(entity);
            await _context.SaveChangesAsync();

            return new PartChangeMasterDto
            {
                Id = entity.Id,
                PartId = entity.PartId,
                PartName = entity.PartName,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt
            };
        }



        public async Task<PartChangeMasterDto?> UpdateAsync(int id, PartChangeMasterUpdateDto dto)
        {
            var entity = await _context.PartChangeMasters.FindAsync(id);
            if (entity == null) return null;

            entity.PartName = dto.PartName ?? entity.PartName;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PartChangeMasters.FindAsync(id);
            if (entity == null) return false;

            _context.PartChangeMasters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {

            var entity = await _context.PartChangeMasters
                .FirstOrDefaultAsync(x => x.PartId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
