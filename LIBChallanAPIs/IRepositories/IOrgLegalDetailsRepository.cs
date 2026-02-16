using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;

public interface IOrgLegalDetailsRepository
{
    Task<OrgLegalDetailsDto?> GetByIdAsync(int id);
    Task<PagedResponse<OrgLegalDetailsDto>> GetAllPagedAsync(PagedRequest request);
    Task<IEnumerable<OrgLegalDetailsDto>> GetAllActiveAsync();
    Task<OrgLegalDetailsDto> CreateAsync(OrgLegalDetailsCreateDto dto);
    Task<OrgLegalDetailsDto?> UpdateAsync(int id, OrgLegalDetailsUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ToggleActiveAsync(ToggleActiveDto dto);
}
