using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface ICorrectiveActionRepository
    {
        Task<CorrectiveActionDto?> GetByIdAsync(int id);
        Task<CorrectiveActionDto?> GetByActionNameAsync(string ActionName);
        Task<PagedResponse<CorrectiveActionDto>> GetAllPagedAsync(PagedRequest request);
        Task<IEnumerable<CorrectiveActionDto>> GetAllActiveAsync();
        Task<CorrectiveActionDto> CreateAsync(CorrectiveActionCreateDto dto);
        Task<CorrectiveActionDto?> UpdateAsync(int id, CorrectiveActionUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
