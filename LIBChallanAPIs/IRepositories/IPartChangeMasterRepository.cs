using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IPartChangeMasterRepository
    {
        Task<PartChangeMasterDto?> GetByIdAsync(int id);
        Task<PartChangeMasterDto?> GetByPartNameAsync(string PartName);
        Task<PagedResponse<PartChangeMasterDto>> GetAllPagedAsync(PagedRequest request);
        Task<IEnumerable<PartChangeMasterDto>> GetAllActiveAsync();
        Task<PartChangeMasterDto> CreateAsync(PartChangeMasterCreateDto dto);
        Task<PartChangeMasterDto?> UpdateAsync(int id, PartChangeMasterUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
