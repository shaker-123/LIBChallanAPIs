using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IGSTTypeMasterRepository
    {
        Task<GSTTypeDto?> GetByIdAsync(int id);
        Task<IEnumerable<GSTTypeDto>> GetAllActiveAsync();
        Task<GSTTypeDto> CreateAsync(GSTTypeCreateDto dto);
        Task<GSTTypeDto?> UpdateAsync(int id, GSTTypeUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
