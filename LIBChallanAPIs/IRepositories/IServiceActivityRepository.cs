using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IServiceActivityRepository
    {
        Task<string> CreateAsync(ServiceActivityCreateDto dto, string loggedInUserId);
    }
}
