public interface IJwtService
{
    Task<LoginResponseDto> GenerateToken(AppUser user);
}
