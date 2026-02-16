using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IBatteryStatusRepository
    {
        Task<BatteryStatusDto?> GetByIdAsync(int id);
        Task<BatteryStatusDto?> GetByStatusNameAsync(string StatusName);
        Task<PagedResponse<BatteryStatusDto>> GetAllPagedAsync(PagedRequest request);
        Task<IEnumerable<BatteryStatusDto>> GetAllActiveAsync();
        Task<BatteryStatusDto> CreateAsync(BatteryStatusCreateDto dto);
        Task<BatteryStatusDto?> UpdateAsync(int id, BatteryStatusUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
