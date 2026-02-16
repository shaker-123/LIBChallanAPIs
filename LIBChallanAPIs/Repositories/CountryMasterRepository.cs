using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class CountryMasterRepository : ICountryMasterRepository
    {
        private readonly AppDbContext _context;

        public CountryMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CountryMasterDto?> GetByIdAsync(int id)
        {
            return await _context.MasterCountries
                .Where(x => x.Id == id)
                .Select(x => new CountryMasterDto
                {
                    Id = x.Id,
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    Continent = x.Continent,
                    PhoneCode = x.PhoneCode,
                    CurrencyCode = x.CurrencyCode,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CountryMasterDto?> GetByCountryNameAsync(string CountryName)
        {
            return await _context.MasterCountries
                .Where(x => x.CountryName == CountryName)
                .Select(x => new CountryMasterDto
                {
                    Id = x.Id,
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    Continent = x.Continent,
                    PhoneCode = x.PhoneCode,
                    CurrencyCode = x.CurrencyCode,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<CountryMasterDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<CountryMaster> query = _context.MasterCountries;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = request.SearchColumn?.ToLower() switch
                {
                    "countrycode" => query.Where(x => x.CountryName.Contains(request.SearchValue)),
                    "countryname" => query.Where(x => x.CountryName.Contains(request.SearchValue)),
                    _ => query.Where(x =>
                        x.CountryName.Contains(request.SearchValue) ||
                        x.CountryName.Contains(request.SearchValue))
                };
            }

            query = (request.SortColumn?.ToLower(), request.SortDirection?.ToUpper()) switch
            {
                ("countrycode", _) => query.OrderBy(x => x.CountryName),
                ("countryname", "DESC") => query.OrderByDescending(x => x.CountryName),
                _ => query.OrderBy(x => x.CountryName)
            };

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
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(x => new CountryMasterDto
                {
                    Id = x.Id,
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    Continent = x.Continent,
                    PhoneCode = x.PhoneCode,
                    CurrencyCode = x.CurrencyCode,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<CountryMasterDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<IEnumerable<CountryMasterDto>> GetAllActiveAsync()
        {
            return await _context.MasterCountries
                .Where(x => x.IsActive)
                .OrderBy(x => x.CountryName)
                .Select(x => new CountryMasterDto
                {
                    Id = x.Id,
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    Continent = x.Continent,
                    PhoneCode = x.PhoneCode,
                    CurrencyCode = x.CurrencyCode,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<CountryMasterDto> CreateAsync(CountryMasterCreateDto dto)
        {
            var lastId = await _context.MasterCountries
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var nextId = lastId + 1;

            var entity = new CountryMaster
            {
                CountryId = $"CNM{nextId:D3}",
                CountryName = dto.CountryName,
                Continent = dto.Continent,
                PhoneCode = dto.PhoneCode,
                CurrencyCode = dto.CurrencyCode,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.MasterCountries.Add(entity);
            await _context.SaveChangesAsync();

            return new CountryMasterDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                CountryName = entity.CountryName,
                Continent = entity.Continent,
                PhoneCode = entity.PhoneCode,
                CurrencyCode = entity.CurrencyCode,
                IsActive = entity.IsActive
            };
        }



        public async Task<CountryMasterDto?> UpdateAsync(int id, CountryMasterUpdateDto dto)
        {
            var entity = await _context.MasterCountries.FindAsync(id);
            if (entity == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.CountryName) &&
                await _context.MasterCountries.AnyAsync(x =>
                    x.CountryName == dto.CountryName && x.Id != id))
                throw new ArgumentException("CountryName already exists.");

            entity.CountryName = dto.CountryName ?? entity.CountryName;
            entity.Continent = dto.Continent ?? entity.Continent;
            entity.PhoneCode = dto.PhoneCode ?? entity.PhoneCode;
            entity.CurrencyCode = dto.CurrencyCode ?? entity.CurrencyCode;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CountryMasterDto
            {
                Id = entity.Id,
                CountryName = entity.CountryName,
                Continent = entity.Continent,
                PhoneCode = entity.PhoneCode,
                CurrencyCode = entity.CurrencyCode,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MasterCountries.FindAsync(id);
            if (entity == null) return false;

            _context.MasterCountries.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(CountryMasterToggleActiveDto dto)
        {
            var entity = await _context.MasterCountries.FindAsync(dto.CountryId);
            if (entity == null) return false;

            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
