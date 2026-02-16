using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IDefectDetailsRepository
    {
        Task<DefectDetailsDto?> GetByIdAsync(int id);
        Task<DefectDetailsDto?> GetByDefectNameAsync(string DefectName);

        Task<PagedResponse<DefectDetailsDto>> GetAllPagedAsync(PagedRequest request);
        Task<IEnumerable<DefectDetailsDto>> GetAllActiveAsync();

        Task<DefectDetailsDto> CreateAsync(DefectDetailsCreateDto dto);
        Task<DefectDetailsDto?> UpdateAsync(int id, DefectDetailsUpdateDto dto);

        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
