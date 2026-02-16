using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.Models
{
    public class UserType : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string TypeId { get; set; } = string.Empty;

        public string TypeName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();

    }
}
