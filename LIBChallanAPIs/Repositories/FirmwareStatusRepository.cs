using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class FirmwareStatusRepository : IFirmwareStatusRepository
    {
        private readonly AppDbContext _context;

        public FirmwareStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FirmwareStatus>> GetAllAsync()
        {
            return await _context.FirmwareStatuses
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<FirmwareStatus?> GetByIdAsync(int id)
        {
            return await _context.FirmwareStatuses.FindAsync(id);
        }

        public async Task<FirmwareStatus> CreateAsync(FirmwareStatus model)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.CreatedBy = "System";

            _context.FirmwareStatuses.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<FirmwareStatus?> UpdateAsync(int id, FirmwareStatus model)
        {
            var existing = await _context.FirmwareStatuses.FindAsync(id);
            if (existing == null) return null;

            existing.FirmwareStatusName = model.FirmwareStatusName;
            existing.IsActive = model.IsActive;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.FirmwareStatuses.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false; // Soft delete
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
