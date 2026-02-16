using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IGSTMasterRepository
    {
        Task<GSTMasterDto?> GetByIdAsync(int id);
        Task<PagedResponse<GSTMasterDto>> GetAllPagedAsync(PagedRequest request);
        Task<IEnumerable<GSTMasterDto>> GetAllActiveAsync();

        Task<GSTMasterDto> CreateAsync(GSTMasterCreateDto dto);
        Task<GSTMasterDto?> UpdateAsync(int id, GSTMasterUpdateDto dto);

        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
