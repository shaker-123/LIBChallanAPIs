using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IAddressMasterRepository
    {
        Task<AddressMasterDto?> GetByIdAsync(int id);
        Task<IEnumerable<AddressMasterDto>> GetAllActiveAsync();
        Task<PagedResponse<AddressMasterDto>> GetAllPagedAsync(PagedRequest request);
        Task<AddressMasterDto> CreateAsync(AddressMasterCreateDto dto);
        Task<AddressMasterDto?> UpdateAsync(int id, AddressMasterUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
