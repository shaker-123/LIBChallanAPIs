using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IStateMasterRepository
    {
        Task<StateMasterDto?> GetByIdAsync(int id);
        Task<IEnumerable<StateMasterDto>> GetAllActiveAsync(string countryId);
        Task<StateMasterDto> CreateAsync(StateMasterCreateDto dto);
        Task<StateMasterDto?> UpdateAsync(int id, StateMasterUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(StateMasterToggleActiveDto dto);
    }
}
