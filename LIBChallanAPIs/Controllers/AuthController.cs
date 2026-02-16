using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        if (await _context.MasterUsers.AnyAsync(x => x.UserName == dto.UserName))
            return BadRequest("Username already exists");

        var lastUserId = await _context.MasterUsers
            .Where(x => x.UserId != null && x.UserId.StartsWith("URR"))
            .OrderByDescending(x => x.UserId)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;
        if (!string.IsNullOrEmpty(lastUserId))
        {
            var numberPart = lastUserId.Substring(3);
            if (int.TryParse(numberPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        string newUserId = $"URR{nextNumber:D3}";

        var user = new AppUser
        {
            UserId = newUserId,
            UserName = dto.UserName,
            FullName = dto.FullName,
            Phone = dto.Phone,
            Email = dto.Email,
            PasswordHash = PasswordHelper.HashPassword(dto.Password),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.MasterUsers.Add(user);
        await _context.SaveChangesAsync();

        return Ok($"User registered successfully with UserId {newUserId}. Role must be assigned by Admin.");
    }




    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.MasterUsers
            .Where(x => x.UserName == dto.UserName && x.IsActive)
            .FirstOrDefaultAsync();

        if (user == null)
            return Unauthorized("Invalid username or password");

        if (!PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid username or password");

        var loginResponse = await _jwtService.GenerateToken(user);
        return Ok(loginResponse);
    }

}
