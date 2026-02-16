using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class GSTMasterRepository : IGSTMasterRepository
    {
        private readonly AppDbContext _context;

        public GSTMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GSTMasterDto?> GetByIdAsync(int id)
        {
            return await _context.GSTMasters
                .Where(x => x.Id == id)
                .Select(x => new GSTMasterDto
                {
                    Id = x.Id,
                    GSTId = x.GSTId,
                    GSTPercentage = x.GSTPercentage,
                    GSTTypeId = x.GSTTypeId,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<GSTMasterDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<GSTMaster> query = _context.GSTMasters;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
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
                .OrderByDescending(x => x.EffectiveFrom)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => new GSTMasterDto
                {
                    Id = x.Id,
                    GSTId = x.GSTId,
                    GSTPercentage = x.GSTPercentage,
                    GSTTypeId = x.GSTTypeId,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<GSTMasterDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<IEnumerable<GSTMasterDto>> GetAllActiveAsync()
        {
            return await _context.GSTMasters
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.EffectiveFrom)
                .Select(x => new GSTMasterDto
                {
                    Id = x.Id,
                    GSTId = x.GSTId,
                    GSTPercentage = x.GSTPercentage,
                    GSTTypeId = x.GSTTypeId,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<GSTMasterDto> CreateAsync(GSTMasterCreateDto dto)
        {
            var lastCode = await _context.GSTMasters
                .Where(x => x.GSTId!.StartsWith("TAX"))
                .OrderByDescending(x => x.GSTId)
                .Select(x => x.GSTId)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastCode))
            {
                var numberPart = lastCode.Substring(3);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            var entity = new GSTMaster
            {
                GSTId = $"TAX{nextNumber:D3}",
                GSTPercentage = dto.GSTPercentage,
                GSTTypeId = dto.GSTTypeId,
                GSTSlabId = dto.GSTSlabId,
                EffectiveFrom = dto.EffectiveFrom,
                EffectiveTo = dto.EffectiveTo,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };

            _context.GSTMasters.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id)
                ?? throw new Exception("GST creation failed");
        }


        public async Task<GSTMasterDto?> UpdateAsync(int id, GSTMasterUpdateDto dto)
        {
            var entity = await _context.GSTMasters.FindAsync(id);
            if (entity == null) return null;

            entity.GSTPercentage = dto.GSTPercentage ?? entity.GSTPercentage;
            entity.EffectiveFrom = dto.EffectiveFrom ?? entity.EffectiveFrom;
            entity.EffectiveTo = dto.EffectiveTo ?? entity.EffectiveTo;
            entity.IsActive = dto.IsActive ?? entity.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.GSTMasters.FindAsync(id);
            if (entity == null) return false;

            _context.GSTMasters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.GSTMasters
                .FirstOrDefaultAsync(x => x.GSTId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
