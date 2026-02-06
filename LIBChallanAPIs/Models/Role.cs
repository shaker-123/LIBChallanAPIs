using System.ComponentModel.DataAnnotations;

public class Role : BaseEntity
{
    [Key]
    public int RoleId { get; set; }

    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; }
}
