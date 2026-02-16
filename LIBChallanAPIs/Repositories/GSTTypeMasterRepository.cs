using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class GSTTypeMasterRepository : IGSTTypeMasterRepository
    {
        private readonly AppDbContext _context;

        public GSTTypeMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GSTTypeDto?> GetByIdAsync(int id)
        {
            return await _context.GSTTypeMasters
                .Where(x => x.Id == id)
                .Select(x => new GSTTypeDto
                {
                    Id = x.Id,
                    GSTTypeId = x.GSTTypeId,
                    GSTTypeCode = x.GSTTypeCode,
                    GSTTypeName = x.GSTTypeName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GSTTypeDto>> GetAllActiveAsync()
        {
            return await _context.GSTTypeMasters
                .Where(x => x.IsActive)
                .OrderBy(x => x.GSTTypeName)
                .Select(x => new GSTTypeDto
                {
                    Id = x.Id,
                    GSTTypeId = x.GSTTypeId,
                    GSTTypeCode = x.GSTTypeCode,
                    GSTTypeName = x.GSTTypeName,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<GSTTypeDto> CreateAsync(GSTTypeCreateDto dto)
        {
            if (await _context.GSTTypeMasters.AnyAsync(x => x.GSTTypeCode == dto.GSTTypeCode))
                throw new ArgumentException("GST Type code already exists.");

            var lastCode = await _context.GSTTypeMasters
                .Where(x => x.GSTTypeId.StartsWith("TXT"))
                .OrderByDescending(x => x.GSTTypeId)
                .Select(x => x.GSTTypeId)
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

            var entity = new GSTTypeMaster
            {
                GSTTypeId = $"TXT{nextNumber:D3}",
                GSTTypeCode = dto.GSTTypeCode,
                GSTTypeName = dto.GSTTypeName,
                IsActive = dto.IsActive
            };

            _context.GSTTypeMasters.Add(entity);
            await _context.SaveChangesAsync();

            return new GSTTypeDto
            {
                Id = entity.Id,
                GSTTypeId = entity.GSTTypeId,
                GSTTypeCode = entity.GSTTypeCode,
                GSTTypeName = entity.GSTTypeName,
                IsActive = entity.IsActive
            };
        }


        public async Task<GSTTypeDto?> UpdateAsync(int id, GSTTypeUpdateDto dto)
        {
            var entity = await _context.GSTTypeMasters.FindAsync(id);
            if (entity == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.GSTTypeCode) &&
                await _context.GSTTypeMasters.AnyAsync(x =>
                    x.GSTTypeCode == dto.GSTTypeCode && x.Id != id))
                throw new ArgumentException("GST Type code already exists.");

            entity.GSTTypeCode = dto.GSTTypeCode ?? entity.GSTTypeCode;
            entity.GSTTypeName = dto.GSTTypeName ?? entity.GSTTypeName;

            await _context.SaveChangesAsync();

            return new GSTTypeDto
            {
                Id = entity.Id,
                GSTTypeCode = entity.GSTTypeCode,
                GSTTypeName = entity.GSTTypeName,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.GSTTypeMasters.FindAsync(id);
            if (entity == null) return false;

            _context.GSTTypeMasters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.GSTTypeMasters
                .FirstOrDefaultAsync(x => x.GSTTypeId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
