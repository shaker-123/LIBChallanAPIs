using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IAddressTypeRepository
    {
        Task<AddressTypeDto?> GetByIdAsync(int id);
        Task<AddressTypeDto?> GetByCodeAsync(string code);

        Task<PagedResponse<AddressTypeDto>> GetAllPagedAsync(PagedRequest request);
        Task<IEnumerable<AddressTypeDto>> GetAllActiveAsync();

        Task<AddressTypeDto> CreateAsync(AddressTypeCreateDto dto);
        Task<AddressTypeDto?> UpdateAsync(int id, AddressTypeUpdateDto dto);

        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
