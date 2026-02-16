using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class CityMasterRepository : ICityMasterRepository
    {
        private readonly AppDbContext _context;

        public CityMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CityMasterDto?> GetByIdAsync(int id)
        {
            return await _context.CityMasters
                .Where(x => x.Id == id)
                .Select(x => new CityMasterDto
                {
                    Id = x.Id,
                    CityId = x.CityId,
                    CityName = x.CityName,
                    StateId = x.StateId,
                    PostalCode = x.PostalCode,
                    AreaCode = x.AreaCode,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CityMasterDto>> GetAllActiveAsync(string stateId)
        {
            return await _context.CityMasters
                .Where(x => x.IsActive && x.StateId == stateId)
                .OrderBy(x => x.CityName)
                .Select(x => new CityMasterDto
                {
                    Id = x.Id,
                    CityId = x.CityId,
                    CityName = x.CityName,
                    StateId = x.StateId,
                    PostalCode = x.PostalCode,
                    AreaCode = x.AreaCode,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<PagedResponse<CityMasterDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<CityMaster> query = _context.CityMasters;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x =>
                    x.CityName.Contains(request.SearchValue) ||
                    x.CityId.Contains(request.SearchValue));
            }

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
                .OrderBy(x => x.CityName)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => new CityMasterDto
                {
                    Id = x.Id,
                    CityId = x.CityId,
                    CityName = x.CityName,
                    StateId = x.StateId,
                    PostalCode = x.PostalCode,
                    AreaCode = x.AreaCode,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<CityMasterDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<CityMasterDto> CreateAsync(CityMasterCreateDto dto)
        {
            var stateExists = await _context.StateMasters
                .AnyAsync(x => x.StateId == dto.StateId);

            if (!stateExists)
                throw new Exception("Invalid StateId");

            var lastId = await _context.CityMasters
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var nextId = lastId + 1;

            var entity = new CityMaster
            {
                CityId = $"CTM{nextId:D3}",
                CityName = dto.CityName,
                StateId = dto.StateId,
                PostalCode = dto.PostalCode,
                AreaCode = dto.AreaCode,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.CityMasters.Add(entity);
            await _context.SaveChangesAsync();

            return new CityMasterDto
            {
                Id = entity.Id,
                CityId = entity.CityId,
                CityName = entity.CityName,
                StateId = entity.StateId,
                PostalCode = entity.PostalCode,
                AreaCode = entity.AreaCode,
                IsActive = entity.IsActive
            };
        }

        public async Task<CityMasterDto?> UpdateAsync(int id, CityMasterUpdateDto dto)
        {
            var entity = await _context.CityMasters.FindAsync(id);
            if (entity == null) return null;

            entity.CityName = dto.CityName ?? entity.CityName;
            entity.PostalCode = dto.PostalCode ?? entity.PostalCode;
            entity.AreaCode = dto.AreaCode ?? entity.AreaCode;
            entity.IsActive = dto.IsActive ?? entity.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CityMasters.FindAsync(id);
            if (entity == null) return false;

            _context.CityMasters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.CityMasters
                .FirstOrDefaultAsync(x => x.CityId == dto.Id);

            if (entity == null) return false;

            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
