using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class AddressTypeRepository : IAddressTypeRepository
    {
        private readonly AppDbContext _context;

        public AddressTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AddressTypeDto?> GetByIdAsync(int id)
        {
            return await _context.EntityTypes
                .Where(x => x.Id == id)
                .Select(x => new AddressTypeDto
                {
                    Id = x.Id,
                    AddressTypeId = x.EntityTypeId,
                    TypeCode = x.TypeCode,
                    TypeName = x.TypeName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AddressTypeDto?> GetByCodeAsync(string code)
        {
            return await _context.EntityTypes
                .Where(x => x.TypeCode == code)
                .Select(x => new AddressTypeDto
                {
                    Id = x.Id,
                    AddressTypeId = x.EntityTypeId,
                    TypeCode = x.TypeCode,
                    TypeName = x.TypeName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AddressTypeDto>> GetAllActiveAsync()
        {
            return await _context.EntityTypes
                .Where(x => x.IsActive)
                .Select(x => new AddressTypeDto
                {
                    Id = x.Id,
                    AddressTypeId = x.EntityTypeId,
                    TypeCode = x.TypeCode,
                    TypeName = x.TypeName,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<PagedResponse<AddressTypeDto>> GetAllPagedAsync(PagedRequest request)
        {
            var query = _context.EntityTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x =>
                    x.TypeCode.Contains(request.SearchValue) ||
                    x.TypeName.Contains(request.SearchValue));
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
                .OrderBy(x => x.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new AddressTypeDto
                {
                    Id = x.Id,
                    AddressTypeId = x.EntityTypeId,
                    TypeCode = x.TypeCode,
                    TypeName = x.TypeName,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<AddressTypeDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }


        public async Task<AddressTypeDto> CreateAsync(AddressTypeCreateDto dto)
        {
            var lastNumber = await _context.EntityTypes
                .OrderByDescending(x => x.EntityTypeId)
                .Select(x => x.EntityTypeId)
                .FirstOrDefaultAsync();

            var nextNumber = lastNumber + 1;

            var entity = new EntityType
            {
                EntityTypeId = nextNumber,
                TypeCode = $"ETY{nextNumber:D3}",
                TypeName = dto.TypeName,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.EntityTypes.Add(entity);
            await _context.SaveChangesAsync();

            return new AddressTypeDto
            {
                Id = entity.Id,
                AddressTypeId = entity.EntityTypeId,
                TypeCode = entity.TypeCode,
                TypeName = entity.TypeName,
                IsActive = entity.IsActive
            };
        }



        public async Task<AddressTypeDto?> UpdateAsync(int id, AddressTypeUpdateDto dto)
        {
            var entity = await _context.EntityTypes.FindAsync(id);
            if (entity == null) return null;

            entity.TypeName = dto.TypeName ?? entity.TypeName;

            await _context.SaveChangesAsync();

            return new AddressTypeDto
            {
                Id = entity.Id,
                AddressTypeId = entity.EntityTypeId,
                TypeCode = entity.TypeCode,
                TypeName = entity.TypeName,
                IsActive = entity.IsActive
            };
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EntityTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return false;

            _context.EntityTypes.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.EntityTypes
        .FirstOrDefaultAsync(x => x.EntityTypeId == dto.Id);

            if (entity == null)
                return false;

            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
