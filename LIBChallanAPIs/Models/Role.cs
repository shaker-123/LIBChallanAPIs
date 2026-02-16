using System.ComponentModel.DataAnnotations;

public class Role : BaseEntity
{
    [Key]                  
    [Required, MaxLength(50)]
    public string RoleId { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string RoleName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
