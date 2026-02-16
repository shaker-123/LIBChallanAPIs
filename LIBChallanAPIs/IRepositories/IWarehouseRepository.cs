using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IWarehouseRepository
    {
        Task<WarehouseDto?> GetByIdAsync(int id);
        Task<WarehouseDto?> GetByCodeAsync(string code);
        Task<IEnumerable<WarehouseDto>> GetAllActiveAsync();
        Task<PagedResponse<WarehouseDto>> GetAllPagedAsync(PagedRequest request);
        Task<WarehouseDto> CreateAsync(WarehouseCreateDto dto);
        Task<WarehouseDto?> UpdateAsync(int id, WarehouseUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
    }
}
