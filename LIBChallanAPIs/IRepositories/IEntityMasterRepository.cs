using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IEntityMasterRepository
    {
        Task<EntityMasterDto?> GetByIdAsync(int id);
        Task<PagedResponse<EntityMasterDto>> GetAllPagedAsync(PagedRequest request);
        Task<IEnumerable<EntityMasterDto>> GetAllActiveAsync();
        Task<EntityMasterDto> CreateAsync(EntityMasterCreateDto dto);
        Task<EntityMasterDto?> UpdateAsync(int id, EntityMasterUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
