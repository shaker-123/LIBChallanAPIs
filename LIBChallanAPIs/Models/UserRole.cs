using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserRole : BaseEntity
{
    [Required]
    public int UserRefId { get; set; }  
    public virtual AppUser User { get; set; } = null!;

    [Required]
    public string RoleId { get; set; } = string.Empty;  
    public virtual Role Role { get; set; } = null!;
}
