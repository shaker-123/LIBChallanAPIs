using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WarehouseDto?> GetByIdAsync(int id)
        {
            return await _context.Warehouses
                .Where(x => x.Id == id)
                .Select(x => new WarehouseDto
                {
                    WarehouseId = x.WarehouseId,
                    WarehouseCode = x.WarehouseCode,
                    CityId = x.CityId,
                    EntityId = x.EntityId,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<WarehouseDto?> GetByCodeAsync(string code)
        {
            return await _context.Warehouses
                .Where(x => x.WarehouseCode == code)
                .Select(x => new WarehouseDto
                {
                    WarehouseId = x.WarehouseId,
                    WarehouseCode = x.WarehouseCode,
                    CityId = x.CityId,
                    EntityId = x.EntityId,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<WarehouseDto>> GetAllActiveAsync()
        {
            return await _context.Warehouses
                .Where(x => x.IsActive)
                .OrderBy(x => x.WarehouseCode)
                .Select(x => new WarehouseDto
                {
                    WarehouseId = x.WarehouseId,
                    WarehouseCode = x.WarehouseCode,
                    CityId = x.CityId,
                    EntityId = x.EntityId,
                    IsActive = x.IsActive
                }).ToListAsync();
        }

        public async Task<PagedResponse<WarehouseDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<Warehouse> query = _context.Warehouses;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x => x.WarehouseCode.Contains(request.SearchValue));
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
                .OrderBy(x => x.WarehouseCode)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => new WarehouseDto
                {
                    WarehouseId = x.WarehouseId,
                    WarehouseCode = x.WarehouseCode,
                    CityId = x.CityId,
                    EntityId = x.EntityId,
                    IsActive = x.IsActive
                }).ToListAsync();

            return new PagedResponse<WarehouseDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<WarehouseDto> CreateAsync(WarehouseCreateDto dto)
        {
            var city = await _context.CityMasters
                .Where(c => c.CityId == dto.CityId)
                .Select(c => c.CityName)
                .FirstOrDefaultAsync();

            if (city == null)
                throw new ArgumentException("Invalid CityId");

            var lastWarehouseId = await _context.Warehouses
                .Where(w => w.WarehouseId!.StartsWith("WHS"))
                .OrderByDescending(w => w.WarehouseId)
                .Select(w => w.WarehouseId)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastWarehouseId))
            {
                var numberPart = lastWarehouseId.Substring(3);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            var entity = new Warehouse
            {
                WarehouseId = $"WHS{nextNumber:D3}",
                CityId = dto.CityId,
                EntityId = dto.EntityId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Warehouses.Add(entity);
            await _context.SaveChangesAsync();

            entity.WarehouseCode = $"{city.Replace(" ", "")}-WH{entity.Id:D4}";
            await _context.SaveChangesAsync();

            return new WarehouseDto
            {
                WarehouseId = entity.WarehouseId,
                WarehouseCode = entity.WarehouseCode,
                CityId = entity.CityId,
                EntityId = entity.EntityId,
                IsActive = entity.IsActive
            };
        }


        public async Task<WarehouseDto?> UpdateAsync(int id, WarehouseUpdateDto dto)
        {
            var entity = await _context.Warehouses.FindAsync(id);
            if (entity == null) return null;

            entity.WarehouseCode = dto.WarehouseCode ?? entity.WarehouseCode;
            entity.CityId = dto.CityId ?? entity.CityId;
            entity.EntityId = dto.EntityId ?? entity.EntityId;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new WarehouseDto
            {
                WarehouseId = entity.WarehouseId,
                WarehouseCode = entity.WarehouseCode,
                CityId = entity.CityId,
                EntityId = entity.EntityId,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Warehouses.FindAsync(id);
            if (entity == null) return false;

            _context.Warehouses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.Warehouses
                .FirstOrDefaultAsync(x => x.WarehouseId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
