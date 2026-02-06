public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public AppUser User { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}
