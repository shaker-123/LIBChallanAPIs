using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class EntityMasterRepository : IEntityMasterRepository
    {
        private readonly AppDbContext _context;

        public EntityMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EntityMasterDto?> GetByIdAsync(int id)
        {
            return await _context.EntityMasters
                .Where(x => x.Id == id)
                .Select(x => new EntityMasterDto
                {
                    Id = x.Id,
                    EntityName = x.EntityName,
                    EntityId = x.EntityId,
                    ContactPerson = x.ContactPerson,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<EntityMasterDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<EntityMaster> query = _context.EntityMasters;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x =>
                    x.EntityName.Contains(request.SearchValue) ||
                    x.Email!.Contains(request.SearchValue));
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
                .OrderBy(x => x.EntityName)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => new EntityMasterDto
                {
                    Id = x.Id,
                    EntityId = x.EntityId,
                    EntityName = x.EntityName,
                    ContactPerson = x.ContactPerson,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<EntityMasterDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<IEnumerable<EntityMasterDto>> GetAllActiveAsync()
        {
            return await _context.EntityMasters
                .Where(x => x.IsActive)
                .OrderBy(x => x.EntityName)
                .Select(x => new EntityMasterDto
                {
                    Id = x.Id,
                    EntityId = x.EntityId,
                    EntityName = x.EntityName,
                    ContactPerson = x.ContactPerson,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<EntityMasterDto> CreateAsync(EntityMasterCreateDto dto)
        {
            var lastCode = await _context.EntityMasters
                .Where(x => x.EntityId.StartsWith("ETM"))
                .OrderByDescending(x => x.EntityId)
                .Select(x => x.EntityId)
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

            var entity = new EntityMaster
            {
                EntityId = $"ETM{nextNumber:D3}",
                EntityName = dto.EntityName,
                ContactPerson = dto.ContactPerson,
                Email = dto.Email,
                Mobile = dto.Mobile,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };

            _context.EntityMasters.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id)
                ?? throw new Exception("Customer creation failed");
        }


        public async Task<EntityMasterDto?> UpdateAsync(int id, EntityMasterUpdateDto dto)
        {
            var entity = await _context.EntityMasters.FindAsync(id);
            if (entity == null) return null;

            entity.EntityName = dto.EntityName ?? entity.EntityName;
            entity.ContactPerson = dto.ContactPerson ?? entity.ContactPerson;
            entity.Email = dto.Email ?? entity.Email;
            entity.Mobile = dto.Mobile ?? entity.Mobile;
            entity.IsActive = dto.IsActive ?? entity.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EntityMasters.FindAsync(id);
            if (entity == null) return false;

            _context.EntityMasters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.EntityMasters
                .FirstOrDefaultAsync(x => x.EntityId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
