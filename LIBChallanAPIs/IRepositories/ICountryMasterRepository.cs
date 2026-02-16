using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface ICountryMasterRepository
    {
        Task<CountryMasterDto?> GetByIdAsync(int id);
        Task<CountryMasterDto?> GetByCountryNameAsync(string CountryName);

        Task<PagedResponse<CountryMasterDto>> GetAllPagedAsync(PagedRequest request);
        Task<IEnumerable<CountryMasterDto>> GetAllActiveAsync();

        Task<CountryMasterDto> CreateAsync(CountryMasterCreateDto dto);
        Task<CountryMasterDto?> UpdateAsync(int id, CountryMasterUpdateDto dto);

        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(CountryMasterToggleActiveDto dto);
    }
}
