using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IServiceActivityRepository
    {
        Task<string> CreateAsync(ServiceActivityCreateDto dto, string loggedInUserId);

        Task UpdateSingleBatteryAsync(string batteryTransId, BatteryTranUpdateDto batteryDto);

        Task<ServiceActivityDto?> GetActivityByIdAsync(string activityId);
        Task<List<ServiceActivityDto>> GetActivitiesByUserIdAsync(string userId);

        Task<PagedResponse<ServiceActivityDto>> GetPagedActivitiesAsync(PagedRequest request);

        Task<BatteryTranDto?> GetBatteryByTransIdAsync(string batteryTransId);

    }
}
