using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class CorrectiveActionRepository : ICorrectiveActionRepository
    {
        private readonly AppDbContext _context;

        public CorrectiveActionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CorrectiveActionDto?> GetByIdAsync(int id)
        {
            return await _context.CorrectiveActions
                .Where(x => x.Id == id)
                .Select(x => new CorrectiveActionDto
                {
                    Id = x.Id,
                    ActionId = x.ActionId,
                    ActionName = x.ActionName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CorrectiveActionDto?> GetByActionNameAsync(string ActionName)
        {
            return await _context.CorrectiveActions
                .Where(x => x.ActionName == ActionName)
                .Select(x => new CorrectiveActionDto
                {
                    Id = x.Id,
                    ActionId = x.ActionId,
                    ActionName = x.ActionName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<CorrectiveActionDto>> GetAllPagedAsync(PagedRequest request)
        {
            IQueryable<CorrectiveAction> query = _context.CorrectiveActions;

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x =>
                    x.ActionName.Contains(request.SearchValue));
            }

            query = query.OrderBy(x => x.ActionName);

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
                .Select(x => new CorrectiveActionDto
                {
                    Id = x.Id,
                    ActionId =  x.ActionId,
                    ActionName = x.ActionName,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new PagedResponse<CorrectiveActionDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

        public async Task<IEnumerable<CorrectiveActionDto>> GetAllActiveAsync()
        {
            return await _context.CorrectiveActions
                .Where(x => x.IsActive)
                .OrderBy(x => x.ActionName)
                .Select(x => new CorrectiveActionDto
                {
                    Id = x.Id,
                    ActionId = x.ActionId,
                    ActionName = x.ActionName,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<CorrectiveActionDto> CreateAsync(CorrectiveActionCreateDto dto)
        {
            if (await _context.CorrectiveActions
                .AnyAsync(x => x.ActionName == dto.ActionName))
                throw new ArgumentException("Action name already exists.");

            var lastId = await _context.CorrectiveActions
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var nextId = lastId + 1;

            var entity = new CorrectiveAction
            {
                ActionId = $"CRA{nextId:D3}",
                ActionName = dto.ActionName,
                IsActive = dto.IsActive,
            };

            _context.CorrectiveActions.Add(entity);
            await _context.SaveChangesAsync();

            return new CorrectiveActionDto
            {
                Id = entity.Id,
                ActionId = entity.ActionId,
                ActionName = entity.ActionName,
                IsActive = entity.IsActive
            };
        }



        public async Task<CorrectiveActionDto?> UpdateAsync(int id, CorrectiveActionUpdateDto dto)
        {
            var entity = await _context.CorrectiveActions.FindAsync(id);
            if (entity == null) return null;

            entity.ActionName = dto.ActionName ?? entity.ActionName;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CorrectiveActions.FindAsync(id);
            if (entity == null) return false;

            _context.CorrectiveActions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(ToggleActiveDto dto)
        {
            var entity = await _context.CorrectiveActions
                .FirstOrDefaultAsync(x => x.ActionId == dto.Id);

            if (entity == null) return false;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
