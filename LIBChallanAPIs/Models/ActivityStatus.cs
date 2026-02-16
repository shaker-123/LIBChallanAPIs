using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class ActivityStatus : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string StatusId { get; set; } = null!; 

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
