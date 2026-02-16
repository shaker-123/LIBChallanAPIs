using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class BatteryStatusRepository : IBatteryStatusRepository
    {
        private readonly AppDbContext _context;

        public BatteryStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BatteryStatusDto?> GetByIdAsync(int id)
        {
            return await _context.BatteryStatuses
                .Where(x => x.Id == id)
                .Select(x => MapToDto(x))
                .FirstOrDefaultAsync();
        }

        public async Task<BatteryStatusDto?> GetByStatusNameAsync(string StatusName)
        {
            return await _context.BatteryStatuses
                .Where(x => x.StatusName == StatusName)
                .Select(x => MapToDto(x))
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<BatteryStatusDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<BatteryStatus> query = _context.BatteryStatuses;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x =>
                    x.StatusName.Contains(request.SearchValue) );
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

            query = query.OrderBy(x => x.StatusName);

            var totalRecords = await query.CountAsync();

            var data = await query
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => MapToDto(x))
                .ToListAsync();

            return new PagedResponse<BatteryStatusDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<IEnumerable<BatteryStatusDto>> GetAllActiveAsync()
        {
            return await _context.BatteryStatuses
                .Where(x => x.IsActive)
                .OrderBy(x => x.StatusName)
                .Select(x => MapToDto(x))
                .ToListAsync();
        }

        public async Task<BatteryStatusDto> CreateAsync(BatteryStatusCreateDto dto)
        {
            
            if (await _context.BatteryStatuses.AnyAsync(x => x.StatusName == dto.StatusName))
                throw new ArgumentException("Status name already exists.");

            
            var lastCode = await _context.BatteryStatuses
                .Where(x => x.StatusId!.StartsWith("BST"))
                .OrderByDescending(x => x.StatusId)
                .Select(x => x.StatusId)
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

            var entity = new BatteryStatus
            {
                StatusId = $"BST{nextNumber:D3}",
                StatusName = dto.StatusName,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.BatteryStatuses.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id)
                ?? throw new Exception("Creation failed");
        }


        public async Task<BatteryStatusDto?> UpdateAsync(int id, BatteryStatusUpdateDto dto)
        {
            var entity = await _context.BatteryStatuses.FindAsync(id);
            if (entity == null) return null;

            entity.StatusName = dto.StatusName ?? entity.StatusName;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BatteryStatuses.FindAsync(id);
            if (entity == null) return false;

            _context.BatteryStatuses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.BatteryStatuses
                .FirstOrDefaultAsync(x => x.StatusId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        private static BatteryStatusDto MapToDto(BatteryStatus x)
        {
            return new BatteryStatusDto
            {
                Id = x.Id,
                StatusId = x.StatusId,
                StatusName = x.StatusName,
                IsActive = x.IsActive
            };
        }
    }
}
