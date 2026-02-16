using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface ICityMasterRepository
    {
        Task<CityMasterDto?> GetByIdAsync(int id);

        Task<PagedResponse<CityMasterDto>> GetAllPagedAsync(PagedRequest request);

        Task<IEnumerable<CityMasterDto>> GetAllActiveAsync(string stateId);

        Task<CityMasterDto> CreateAsync(CityMasterCreateDto dto);

        Task<CityMasterDto?> UpdateAsync(int id, CityMasterUpdateDto dto);

        Task<bool> DeleteAsync(int id);

        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
