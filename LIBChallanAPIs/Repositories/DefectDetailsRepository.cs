using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class DefectDetailsRepository : IDefectDetailsRepository
    {
        private readonly AppDbContext _context;

        public DefectDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DefectDetailsDto?> GetByIdAsync(int id)
        {
            return await _context.DefectDetails
                .Where(x => x.Id == id)
                .Select(x => new DefectDetailsDto
                {
                    Id = x.Id,
                    DefectName = x.DefectName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DefectDetailsDto?> GetByDefectNameAsync(string DefectName)
        {
            return await _context.DefectDetails
                .Where(x => x.DefectName == DefectName)
                .Select(x => new DefectDetailsDto
                {
                    Id = x.Id,
                    DefectTypeId = x.DefectTypeId,
                    DefectName = x.DefectName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<DefectDetailsDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<DefectDetail> query = _context.DefectDetails;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x =>
                    x.DefectName.Contains(request.SearchValue));
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
                .OrderBy(x => x.DefectName)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => new DefectDetailsDto
                {
                    Id = x.Id,
                    DefectTypeId = x.DefectTypeId,
                    DefectName = x.DefectName,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<DefectDetailsDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<IEnumerable<DefectDetailsDto>> GetAllActiveAsync()
        {
            return await _context.DefectDetails
                .Where(x => x.IsActive)
                .OrderBy(x => x.DefectName)
                .Select(x => new DefectDetailsDto
                {
                    Id=x.Id,
                    DefectTypeId = x.DefectTypeId,
                    DefectName = x.DefectName,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<DefectDetailsDto> CreateAsync(DefectDetailsCreateDto dto)
        {
            if (await _context.DefectDetails.AnyAsync(x => x.DefectName == dto.DefectName))
                throw new ArgumentException("Defect name already exists.");

            var lastCode = await _context.DefectDetails
                .Where(x => x.DefectTypeId.StartsWith("DDT"))
                .OrderByDescending(x => x.DefectTypeId)
                .Select(x => x.DefectTypeId)
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

            var entity = new DefectDetail
            {
                DefectTypeId = $"DDT{nextNumber:D3}", 
                DefectName = dto.DefectName,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.DefectDetails.Add(entity);
            await _context.SaveChangesAsync();

            return new DefectDetailsDto
            {
                Id = entity.Id,
                DefectTypeId = entity.DefectTypeId,
                DefectName = entity.DefectName,
                IsActive = entity.IsActive
            };
        }


        public async Task<DefectDetailsDto?> UpdateAsync(int id, DefectDetailsUpdateDto dto)
        {
            var entity = await _context.DefectDetails.FindAsync(id);
            if (entity == null) return null;

            entity.DefectName = dto.DefectName ?? entity.DefectName;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new DefectDetailsDto
            {
                Id = entity.Id,
                DefectName = entity.DefectName,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DefectDetails.FindAsync(id);
            if (entity == null) return false;

            _context.DefectDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.DefectDetails
                .FirstOrDefaultAsync(x => x.DefectTypeId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
