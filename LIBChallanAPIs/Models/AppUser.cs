using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class AppUser : BaseEntity
{
    [Key]
    public int Id { get; set; } 

    [Required, MaxLength(10)]
    public string UserId { get; set; } = string.Empty;  

    [Required, MaxLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public byte[] PasswordHash { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
