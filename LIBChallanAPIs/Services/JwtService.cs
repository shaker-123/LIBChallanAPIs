using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _context;

    public JwtService(IConfiguration config, AppDbContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task<LoginResponseDto> GenerateToken(AppUser user)
    {
        var roles = await _context.MasterUserRoles
            .Where(x => x.UserId == user.UserId)
            .Select(x => x.Role.RoleName)
            .ToListAsync();

        var claims = new List<Claim>
        {
            new Claim("UserId", user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var expires = DateTime.UtcNow.AddMinutes(
            Convert.ToDouble(_config["Jwt:DurationInMinutes"]));

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return new LoginResponseDto
        {
            Token = token,
            Expiration = expires,
            UserId = user.UserId,
            Name = user.FullName,
            Roles = roles
        };
    }
}
