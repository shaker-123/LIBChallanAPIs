using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class OrgLegalDetailsRepository : IOrgLegalDetailsRepository
    {
        private readonly AppDbContext _context;

        public OrgLegalDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrgLegalDetailsDto?> GetByIdAsync(int id)
        {
            return await _context.OrgLegalDetails
                .Where(x => x.Id == id)
                .Select(x => new OrgLegalDetailsDto
                {
                    Id = x.Id,
                    LegalId = x.LegalId,
                    EntityId = x.EntityId,
                    GSTINNumber = x.GSTINNumber,
                    CINNumber = x.CINNumber,
                    PANNumber = x.PANNumber,
                    CityId = x.CityId,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<OrgLegalDetailsDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<OrgLegalDetail> query = _context.OrgLegalDetails;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x =>
                    x.GSTINNumber.Contains(request.SearchValue) ||
                    x.PANNumber.Contains(request.SearchValue));
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
                .OrderByDescending(x => x.Id)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => new OrgLegalDetailsDto
                {
                    Id = x.Id,
                    LegalId = x.LegalId,
                    EntityId = x.EntityId,
                    GSTINNumber = x.GSTINNumber,
                    CINNumber = x.CINNumber,
                    PANNumber = x.PANNumber,
                    CityId = x.CityId,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<OrgLegalDetailsDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<IEnumerable<OrgLegalDetailsDto>> GetAllActiveAsync()
        {
            return await _context.OrgLegalDetails
                .Where(x => x.IsActive)
                .Select(x => new OrgLegalDetailsDto
                {
                    Id = x.Id,
                    LegalId = x.LegalId,
                    EntityId = x.EntityId,
                    GSTINNumber = x.GSTINNumber,
                    CINNumber = x.CINNumber,
                    PANNumber = x.PANNumber,
                    CityId = x.CityId,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<OrgLegalDetailsDto> CreateAsync(OrgLegalDetailsCreateDto dto)
        {
            if (await _context.OrgLegalDetails.AnyAsync(x => x.EntityId == dto.EntityId))
                throw new ArgumentException("Legal details already exist for this entity.");

            var lastLegalId = await _context.OrgLegalDetails
                .Where(x => x.LegalId != null && x.LegalId.StartsWith("OLD"))
                .OrderByDescending(x => x.LegalId)
                .Select(x => x.LegalId)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastLegalId))
            {
                var numberPart = lastLegalId.Substring(3);
                if (int.TryParse(numberPart, out int lastNumber))
                    nextNumber = lastNumber + 1;
            }

            string newLegalId = $"OLD{nextNumber:D3}";

            var entity = new OrgLegalDetail
            {
                LegalId = newLegalId,
                EntityId = dto.EntityId,
                GSTINNumber = dto.GSTINNumber,
                CINNumber = dto.CINNumber,
                PANNumber = dto.PANNumber,
                CityId = dto.CityId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.OrgLegalDetails.Add(entity);
            await _context.SaveChangesAsync();

            return new OrgLegalDetailsDto
            {
                Id = entity.Id,
                LegalId = entity.LegalId,
                EntityId = entity.EntityId,
                GSTINNumber = entity.GSTINNumber,
                CINNumber = entity.CINNumber,
                PANNumber = entity.PANNumber,
                CityId = entity.CityId,
                IsActive = entity.IsActive
            };
        }



        public async Task<OrgLegalDetailsDto?> UpdateAsync(int id, OrgLegalDetailsUpdateDto dto)
        {
            var entity = await _context.OrgLegalDetails.FindAsync(id);
            if (entity == null) return null;

            entity.GSTINNumber = dto.GSTINNumber ?? entity.GSTINNumber;
            entity.CINNumber = dto.CINNumber ?? entity.CINNumber;
            entity.PANNumber = dto.PANNumber ?? entity.PANNumber;
            entity.CityId = dto.CityId ?? entity.CityId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.OrgLegalDetails.FindAsync(id);
            if (entity == null) return false;

            _context.OrgLegalDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.OrgLegalDetails
                .FirstOrDefaultAsync(x => x.LegalId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
