using LIBChallanAPIs.DTOs;

namespace LIBChallanAPIs.IRepositories
{
    public interface IUserTypeRepository
    {
        Task<IEnumerable<UserTypeDto>> GetAllAsync();
        Task<UserTypeDto?> GetByIdAsync(int id);
        Task<UserTypeDto> CreateAsync(UserTypeCreateDto dto, int userId);
    }
}
