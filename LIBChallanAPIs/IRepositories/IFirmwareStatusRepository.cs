using LIBChallanAPIs.Models;

namespace LIBChallanAPIs.IRepositories
{
    public interface IFirmwareStatusRepository
    {
        Task<IEnumerable<FirmwareStatus>> GetAllAsync();
        Task<FirmwareStatus?> GetByIdAsync(int id);
        Task<FirmwareStatus> CreateAsync(FirmwareStatus model);
        Task<FirmwareStatus?> UpdateAsync(int id, FirmwareStatus model);
        Task<bool> DeleteAsync(int id);
    }
}
