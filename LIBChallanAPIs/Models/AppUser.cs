using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class AppUser : BaseEntity
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; }

    [MaxLength(50)]
    public string Phone { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    public byte[] PasswordHash { get; set; }

    public bool IsActive { get; set; } = true;

    
    [ForeignKey(nameof(CreatedBy))]
    public AppUser CreatedByUser { get; set; }

    [ForeignKey(nameof(UpdatedBy))]
    public AppUser UpdatedByUser { get; set; }
}
