using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class AddressMasterRepository : IAddressMasterRepository
    {
        private readonly AppDbContext _context;

        public AddressMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AddressMasterDto?> GetByIdAsync(int id)
        {
            return await _context.AddressMasters
                .Where(a => a.Id == id)
                .Select(a => new AddressMasterDto
                {
                    Id = a.Id,
                    AddressId = a.AddressId,
                    EntityId = a.EntityId,
                    WarehouseId = a.WarehouseId,
                    AddressTypeId = a.EntityTypeId,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    CityId = a.CityId,
                    CityName = a.City != null ? a.City.CityName : string.Empty,
                    PostalCode = a.PostalCode,
                    IsActive = a.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AddressMasterDto>> GetAllActiveAsync()
        {
            return await _context.AddressMasters
                .Where(a => a.IsActive)
                .Select(a => new AddressMasterDto
                {
                    Id = a.Id,
                    AddressId = a.AddressId,
                    EntityId = a.EntityId,
                    WarehouseId = a.WarehouseId,
                    AddressTypeId = a.EntityTypeId,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    CityId = a.CityId,
                    CityName = a.City != null ? a.City.CityName : string.Empty,
                    PostalCode = a.PostalCode,
                    IsActive = a.IsActive
                }).ToListAsync();
        }

        public async Task<PagedResponse<AddressMasterDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<AddressMaster> query = _context.AddressMasters;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(a => a.AddressLine1.Contains(request.SearchValue) ||
                                         a.AddressLine2.Contains(request.SearchValue));
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
                .OrderBy(a => a.Id)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(a => new AddressMasterDto
                {
                    Id = a.Id,
                    AddressId = a.AddressId,
                    EntityId = a.EntityId,
                    WarehouseId = a.WarehouseId,
                    AddressTypeId = a.EntityTypeId,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    CityId = a.CityId,
                    CityName = a.City != null ? a.City.CityName : string.Empty,
                    PostalCode = a.PostalCode,
                    IsActive = a.IsActive
                }).ToListAsync();

            return new PagedResponse<AddressMasterDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<AddressMasterDto> CreateAsync(AddressMasterCreateDto dto)
        {
            var lastAddressId = await _context.AddressMasters
                .Where(a => a.AddressId!.StartsWith("ADR"))
                .OrderByDescending(a => a.AddressId)
                .Select(a => a.AddressId)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastAddressId))
            {
                var numberPart = lastAddressId.Substring(3);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            var entity = new AddressMaster
            {
                AddressId = $"ADR{nextNumber:D3}", 
                EntityId = dto.EntityId,
                WarehouseId = dto.WarehouseId,
                EntityTypeId = dto.AddressTypeId,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                CityId = dto.CityId,
                PostalCode = dto.PostalCode,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };

            _context.AddressMasters.Add(entity);
            await _context.SaveChangesAsync();

            var cityName = await _context.CityMasters
                .Where(c => c.CityId == dto.CityId)
                .Select(c => c.CityName)
                .FirstOrDefaultAsync();

            var addressTypeName = await _context.EntityTypes
                .Where(t => t.EntityTypeId == dto.AddressTypeId)
                .Select(t => t.TypeName)
                .FirstOrDefaultAsync();

            return new AddressMasterDto
            {
                Id = entity.Id,
                AddressId = entity.AddressId, 
                EntityId = entity.EntityId,
                WarehouseId = entity.WarehouseId,
                AddressTypeId = entity.EntityTypeId,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                CityId = entity.CityId,
                CityName = cityName ?? string.Empty,
                PostalCode = entity.PostalCode,
                IsActive = entity.IsActive
            };
        }


        public async Task<AddressMasterDto?> UpdateAsync(int id, AddressMasterUpdateDto dto)
        {
            var entity = await _context.AddressMasters.FindAsync(id);
            if (entity == null) return null;

            entity.EntityId = dto.EntityId ?? entity.EntityId;
            entity.WarehouseId = dto.WarehouseId ?? entity.WarehouseId;
            entity.EntityTypeId = dto.AddressTypeId ?? entity.EntityTypeId;
            entity.AddressLine1 = dto.AddressLine1 ?? entity.AddressLine1;
            entity.AddressLine2 = dto.AddressLine2 ?? entity.AddressLine2;
            entity.CityId = dto.CityId ?? entity.CityId;
            entity.PostalCode = dto.PostalCode ?? entity.PostalCode;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var cityName = await _context.CityMasters
                .Where(c => c.CityId == entity.CityId)
                .Select(c => c.CityName)
                .FirstOrDefaultAsync();

            var addressTypeName = await _context.EntityTypes
                .Where(t => t.EntityTypeId == entity.EntityTypeId)
                .Select(t => t.TypeName)
                .FirstOrDefaultAsync();

            return new AddressMasterDto
            {
                Id = entity.Id,
                EntityId = entity.EntityId,
                WarehouseId = entity.WarehouseId,
                AddressTypeId = entity.EntityTypeId,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                CityId = entity.CityId,
                CityName = cityName ?? string.Empty,
                PostalCode = entity.PostalCode,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AddressMasters.FindAsync(id);
            if (entity == null) return false;

            _context.AddressMasters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.AddressMasters.FindAsync(dto.Id);
            if (entity == null) return false;

            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
