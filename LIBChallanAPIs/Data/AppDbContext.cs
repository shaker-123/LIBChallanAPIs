using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContext;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IHttpContextAccessor httpContext)
        : base(options)
    {
        _httpContext = httpContext;
    }

    public DbSet<AppUser> MasterUsers { get; set; }
    public DbSet<Role> MasterRoles { get; set; }
    public DbSet<UserRole> MasterUserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasIndex(x => x.UserName)
            .IsUnique();

        modelBuilder.Entity<UserRole>()
            .HasKey(x => new { x.UserId, x.RoleId });

       

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                RoleId = 1,
                RoleName = "SUPER_USER",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                RoleId = 2,
                RoleName = "ADMIN",
                CreatedAt = DateTime.UtcNow
            }
        );

        base.OnModelCreating(modelBuilder);
    }


    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var userId = GetCurrentUserId();

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private int? GetCurrentUserId()
    {
        var userIdClaim = _httpContext.HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == "UserId");

        return userIdClaim != null ? int.Parse(userIdClaim.Value) : null;
    }
}
